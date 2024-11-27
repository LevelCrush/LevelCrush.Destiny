using System.Text.Json;
using Destiny.Api;
using Destiny.Models.Manifests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NRedisStack.Search.Aggregation;
using Rasputin.Database;
using Rasputin.Database.Models;
using Rasputin.MessageQueue.Enums;
using Rasputin.MessageQueue.Models;
using Rasputin.MessageQueue.Queues;

namespace Rasputin.MessageQueue.Consumer;

public class ConsumerActions
{
    public static async Task<bool> Process(MessageAction action)
    {
        switch (action.Action.ToLower().Trim())
        {
            case "manifest:sync":
                await TaskManifestSync();
                break;
            case "crawl:members":
                await TaskCrawlMembers();
                break;
            case "crawl:clans":
                await TaskCrawlClans();
                break;
            case "crawl:network:clans":
                await TaskCrawlClans(true);
                break;
            default:
                LoggerGlobal.Write($"Unknown action: {action.Action}");
                break;
        }
        return true;
    }
    
    public static async Task<bool> TaskCrawlClans(bool onlyNetwork = false)
    {
        LoggerGlobal.Write($"Querying database to fetch clans");
        await using (var db = await RasputinDatabase.Connect())
        {
            
            var clans = onlyNetwork 
                    ? await db.Clans.
                        Where((c) => c.IsNetwork)
                        .OrderBy((c) => c.UpdatedAt)
                        .Take(100)
                        .AsNoTracking()
                        .ToArrayAsync()
                    : await db.Clans.OrderBy((c) => c.UpdatedAt)
                .Take(100)
                .AsNoTracking()
                .ToArrayAsync();
            
            
            
            LoggerGlobal.Write($"Found {clans.Length} clans.");
            
            // same as members
            // even though our consumers support batching in messages
            // just send one at a time  

            foreach (var clan in clans)
            {
                QueueClan.Publish(new MessageClan()
                {
                    Task = MessageClanTask.Crawl, 
                    Entities = [clan.GroupId.ToString()]
                });
                LoggerGlobal.Write($"Published clan {clan.GroupId} to crawl");
            }
        }

        return true;
    }

    public static async Task<bool> TaskCrawlMembers()
    {
       LoggerGlobal.Write($"Querying database to fetch members");
       await using (var db = await RasputinDatabase.Connect())
       {
           var results = await db.Members.OrderBy((m) => m.UpdatedAt)
               .Take(1000)
               .AsNoTracking()
               .ToArrayAsync();
           
           LoggerGlobal.Write($"Fetched {results.Length} members");

           // yes we can in theory send all of these results into a single message
           // but for better reliability, send each one as its own message
           // this will also allow multiple consumers to handle multiple members at the same time
           // if we need to optimize we can chunk these by a smaller number later
           foreach (var member in results)
           {
               
               // activity task are the most efficient for members to get all new information + activities
               QueueMember.Publish(new MessageMember()
               {
                   Task = MessageMemberTask.Activities, 
                   Entities = [member.MembershipId.ToString()]
               });
               LoggerGlobal.Write($"Published Member {member.MembershipId} to crawl");
           }
       }

       return true;
    }

    public static async Task<bool> TaskManifestSync()
    {

        LoggerGlobal.Write($"Fetching class definitions");
        var classDefinitions = await DestinyManifest.Get<DestinyClassDefinition>();
        
        LoggerGlobal.Write($"Publishing class definitions");
        QueueDBSync.Publish(new MessageDbSync()
        {
            Task = MessageDbSyncTask.ManifestClassDefinitions,
            Data = JsonSerializer.Serialize(classDefinitions)
        });
        
        
        LoggerGlobal.Write($"Fetching activity definitions");
        var activityDefinitions = await DestinyManifest.Get<DestinyActivityDefinition>();
        
        LoggerGlobal.Write($"Publishing activity definitions");
        QueueDBSync.Publish(new MessageDbSync()
        {
            Task = MessageDbSyncTask.ManifestActivityDefinitions,
            Data = JsonSerializer.Serialize(activityDefinitions)
        });

        LoggerGlobal.Write($"Fetching activity type definitions");
        var activityTypeDefintions = await DestinyManifest.Get<DestinyActivityTypeDefinition>();
        QueueDBSync.Publish(new MessageDbSync()
        {
            Task = MessageDbSyncTask.ManifestActivityTypeDefinitions,
            Data = JsonSerializer.Serialize(activityTypeDefintions)
        });

        LoggerGlobal.Write($"Fetching season definitions");
        var seasonDefinitions = await DestinyManifest.Get<DestinySeasonDefinition>();
        
        LoggerGlobal.Write($"Publishing season definitions");
        QueueDBSync.Publish(new MessageDbSync()
        {
            Task = MessageDbSyncTask.ManifestSeasonDefinitions,
            Data = JsonSerializer.Serialize(seasonDefinitions)
        });
        
        LoggerGlobal.Write($"Fetching triumph record definitions");
        var triumphDefinitions = await DestinyManifest.Get<DestinyRecordDefinition>();
        
        LoggerGlobal.Write($"Publishing triumph record definitions");
        QueueDBSync.Publish(new MessageDbSync()
        {
            Task = MessageDbSyncTask.ManifestTriumphDefinitions,
            Data= JsonSerializer.Serialize(triumphDefinitions)
        });
        
        
        LoggerGlobal.Write("Done publishing known manifest data");
        
        return true;
    }
}