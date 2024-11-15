using System.Collections.Concurrent;
using System.Text.Json;
using Destiny.Api;
using Destiny.Models.Responses;
using Destiny.Models.Schemas;
using Microsoft.Extensions.Logging;
using Rasputin.MessageQueue.Enums;
using Rasputin.MessageQueue.Models;
using Rasputin.MessageQueue.Queues;

namespace Rasputin.MessageQueue.Consumer;

public static class ConsumerClan
{
    
    
    
    public static async Task<bool> Process(MessageClan clan)
    {
        switch (clan.Task)
        {
            case MessageClanTask.Info:
                await ProcessClanInfo(clan.Entities);
                break;
            case MessageClanTask.Roster:
                await ProcessClanRoster(clan.Entities);
                break;
            case MessageClanTask.Crawl:
                await ProcessClanCrawl(clan.Entities);
                break;
            case MessageClanTask.CrawlFresh:
                await ProcessClanCrawl(clan.Entities, true);
                break;
            default:
                LoggerGlobal.Write($"Clan task {clan.Task} is not implemented.");
                break;
        }
        return false;
    }

    public static async Task<ConcurrentDictionary<long, DestinyGroupResponse>> ProcessClanInfo(string[] entities)
    {
        var infos = new ConcurrentDictionary<long, DestinyGroupResponse>();
        foreach (var entity in entities)
        {
            LoggerGlobal.Write($"Processing clan entity {entity}");
            long.TryParse(entity, out var clanId);
            
            
            var res = await DestinyClan.Info(clanId);
            if (res != null)
            {
                LoggerGlobal.Write($"Publishing clan entity {entity}");
                QueueDBSync.Publish(new MessageDbSync()
                {
                    Task = MessageDbSyncTask.ClanInfo,
                    Data = JsonSerializer.Serialize(res)
                });

                infos.TryAdd(clanId, res);
            }
            else
            {
                LoggerGlobal.Write($"Clan entity {entity} was not found or could not be deserialized");
            }
            
        }

        return infos;
    }

    public static async Task<ConcurrentDictionary<long, DestinySearchResultOfGroupMember>> ProcessClanRoster(string[] entities)
    {
        var rosters = new ConcurrentDictionary<long, DestinySearchResultOfGroupMember>();
        foreach (var entity in entities)
        {
            LoggerGlobal.Write($"Processing clan roster entity {entity}");
            
            long.TryParse(entity, out var clanId);
            
            var res = await DestinyClan.Roster(clanId);
            if (res != null)
            {
                LoggerGlobal.Write($"Publishing clan roster entity {entity}");
                QueueDBSync.Publish(new MessageDbSync()
                {
                    Task = MessageDbSyncTask.ClanRoster,
                    Data = JsonSerializer.Serialize(res)
                });
                rosters.TryAdd(clanId, res);
            }
            else
            {
                LoggerGlobal.Write($"Clan roster entity {entity} was not found or could not be deserialized");
            }
        }

        return rosters;
    }

    public static async Task<bool> ProcessClanCrawl(string[] entities, bool fresh = false)
    {
        
        var infos = await ProcessClanInfo(entities);
        var rosters = await ProcessClanRoster(entities);
        
        foreach (var entity in entities)
        {
            LoggerGlobal.Write($"Processing clan crawl entity {entity}");
            long.TryParse(entity, out var clanId);
            
            infos.TryGetValue(clanId, out var clanInfo);
            rosters.TryGetValue(clanId, out var clanRoster);

            if (clanInfo == null || clanRoster == null)
            {
                LoggerGlobal.Write($"Could not get clan information or clan roster for clan {entity}");
                continue;
            }
            
            LoggerGlobal.Write($"Crawling clan roster profiles for clan {entity}");
            var allProfileInstanceIds = new ConcurrentDictionary<string, HashSet<long>>();

            try
            {
                foreach (var clanMember in clanRoster.Results)
                {
                    // turns out its better to go one at a time for reliablity
                    var memberInstances = await ConsumerMember.TaskActivities([clanMember.UserInfo.MembershipId.ToString()], fresh);
                    allProfileInstanceIds.Union(memberInstances);
                }
            
            }
            catch (Exception ex)
            {
                LoggerGlobal.Write($"Could not get all membership activities for clan {entity}");
            }

            // union all instance ids together and ensure we only have unique ones
            var instanceIds = new HashSet<long>();
            foreach (var (member, profileInstances) in allProfileInstanceIds)
            {
                instanceIds.UnionWith(profileInstances);
            }

            // this is technically redudant with how we have the other consumers running including DB Sync. In our system if we have crawled any instances
            // we store a key in our redis to indicate that we have
            // so long as that key isnt expired. The instance wont be recrawled and will be skipped. 
            // so it is **ultimately** harmless to do this if we are frequently pushing the same instance ids
            // note: in our case, ConsumerDBSync will ultimately push any unhandled instance ids it finds as it writes to the database
            // worst case this just pushes the instance consumer to process the instance earlier
            var chunkCount = 0;
            foreach (var chunk in instanceIds.Chunk(ConsumerDbSync.ChunkSizeInstances))
            {
                var chunkEntities = new List<string>();
                foreach (var instanceId in chunk)
                {
                    chunkEntities.Add(instanceId.ToString());
                }
                LoggerGlobal.Write($"Publishing instance ids to queue for clan {entity}: Chunk {++chunkCount}");
                QueueInstance.Publish(new MessageInstance()
                {
                    Entities = chunkEntities.ToArray()
                });
            }
            LoggerGlobal.Write($"Done crawling clan {entity}");
        }
        
        return true;
    }
    
    
}