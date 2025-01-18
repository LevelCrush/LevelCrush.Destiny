using System.Collections.Concurrent;
using System.Text.Json;
using Destiny.Api;
using Destiny.Models.Enums;
using Destiny.Models.Responses;
using Destiny.Models.Schemas;
using Microsoft.EntityFrameworkCore;
using Rasputin.Database;
using Rasputin.Database.Models;
using Rasputin.MessageQueue.Enums;
using Rasputin.MessageQueue.Models;
using Rasputin.MessageQueue.Queues;

namespace Rasputin.MessageQueue.Consumer;

public static class ConsumerMember
{
    public static async Task<bool> Process(MessageMember message)
    {

        switch (message.Task)
        {
            case MessageMemberTask.Info:
                await TaskInfo(message.Entities);
                break;
            case MessageMemberTask.Activities:
                await TaskActivities(message.Entities);
                break;
            case MessageMemberTask.ActivitiesFresh:
                await TaskActivities(message.Entities, true);
                break;
            default:
                LoggerGlobal.Write($"Unknown member task {message.Task}");
                break;
        }
        
        
        
        return false;
    }

    public static async Task<ConcurrentDictionary<string, HashSet<long>>> TaskActivities(string[] entities, bool fresh = false)
    {
        /*
         * When processing an activity message , the entities themselves are membership ids
         * This means we can query them for Info at the same time and get **alot** more value of this message
         * Making activity messages the most efficient way of crawling a member directly
         * 
         */
        var profiles = await TaskInfo(entities);
        
        var profilesInstanceIds = new ConcurrentDictionary<string, HashSet<long>>();
        foreach (var entity in entities)
        {
            var instanceIds = new HashSet<long>();
            var found = profiles.TryGetValue(entity, out var profile);
            if (found && profile != null 
                      && profile.Profile != null 
                      && profile.Profile.Data != null
                      && profile.Characters != null 
                      && profile.Characters.Data != null)
            {
                LoggerGlobal.Write($"Found profile for {entity}. Beginning to crawl characters");
                
                foreach (var character in profile.Characters.Data.Values)
                {
                    LoggerGlobal.Write($"Crawling character for {entity}: {character.CharacterId}");
                    var characterInstanceIds = await TaskActivitiesCharacter(character.MembershipId,
                        character.MembershipType, character.CharacterId, fresh);
                    LoggerGlobal.Write($"Done Crawling character for {entity}: {character.CharacterId}");
                    
                    instanceIds.UnionWith(characterInstanceIds);
                    LoggerGlobal.Write($"Done combining instance ids for {entity}: {character.CharacterId}");
                }

            }
            else
            {
                LoggerGlobal.Write($"Profile could not be found for {entity}");
            }
            profilesInstanceIds.TryAdd(entity, instanceIds);
        }

        return profilesInstanceIds;
    }

    public static async Task<HashSet<long>> TaskActivitiesCharacter(long membershipId, BungieMembershipType membershipType,
        long characterId, bool fresh = false)
    {
        long startTimestamp = 0;
        await using (var db = await RasputinDatabase.Connect())
        {
            // if fresh, we always start at 0 
            // if not fresh, query the database to see if we can pull the most recent activity (if any)
            if (fresh)
            {
                startTimestamp = 0;
            }
            else
            {
                var recentActivity = await db.MemberActivities
                    .Where(x => x.CharacterId == characterId)
                    .OrderByDescending(x => x.OccurredAt)
                    .Take(1)
                    .FirstOrDefaultAsync();
                
                startTimestamp = recentActivity != null ? recentActivity.OccurredAt : 0;
            }
        }

        LoggerGlobal.Write($"Starting to crawl character {characterId} activities using timestamp {startTimestamp} as base");
        var activityHistory = await DestinyActivity.ForCharacter(membershipId, membershipType, characterId, startTimestamp);
        var chunkCounter = 0;
        foreach(var chunk in activityHistory.Chunk(ConsumerDbSync.ChunkSizeCharacterHistoryActivity))
        {
            var payload = JsonSerializer.Serialize(chunk);
        
            LoggerGlobal.Write($"Chunk: {++chunkCounter} Done crawling character {characterId} activities. Publishing activity and stats");

            var headers = new Dictionary<string, string>()
            {
                { "membership", membershipId.ToString() },
                { "platform", ((int)membershipType).ToString() },
                { "character", characterId.ToString() }
            };
            
            // message dedicated to activity history
            QueueDBSync.Publish(new MessageDbSync()
            {
                Task = MessageDbSyncTask.ActivityHistory,
                Data = payload,
                Headers = headers
            });
        
            LoggerGlobal.Write($"Chunk: {chunkCounter} Done publishing activity for {characterId}");

            // message dedicated to activity stats
            QueueDBSync.Publish(new MessageDbSync()
            {
                Task = MessageDbSyncTask.ActivityStats,
                Data = payload,
                Headers = headers
            });
        
            LoggerGlobal.Write($"Chunk: {chunkCounter} Done publishing stats for {characterId}");
        }
        

        var instanceIds = new HashSet<long>();
        foreach (var activity in activityHistory)
        {
            instanceIds.Add(activity.Details.InstanceId);
        }

        return instanceIds;
    }
    

    public static async Task<ConcurrentDictionary<string, DestinyProfileResponse?>> TaskInfo(string[] entities)
    {
        ConcurrentDictionary<string, DestinyProfileResponse?> profiles = new ConcurrentDictionary<string, DestinyProfileResponse?>();
        foreach (var entity in entities)
        {
            var isBungieName = entity.Contains("#");
            UserInfoCard? user = null;
            if (isBungieName)
            {
                LoggerGlobal.Write($"Entity `{entity}` has been detected as a normal bungie name input. Searching first");
                user = await DestinyMember.Search(entity);
            }
            else
            {
                long membershipId = 0;
                var converted = long.TryParse(entity, out membershipId);
                if (converted)
                {
                    LoggerGlobal.Write($"Querying {membershipId} membership");
                    user = await DestinyMember.MembershipById(membershipId);
                }
                else
                {
                    LoggerGlobal.Write($"Failed to convert entity `{entity}` to a numerical value");
                }
            }

            DestinyProfileResponse? profile = null;
            if (user != null)
            {
                LoggerGlobal.Write($"{user.GlobalDisplayName}#{user.GlobalDisplayNameCode} has been found. Querying profile");
                profile = await DestinyMember.Profile(user.MembershipId, (int)user.MembershipType);
            }

            if (profile != null && profile.Profile != null && profile.Profile.Data != null)
            {
                LoggerGlobal.Write($"{profile.Profile.Data.UserInfo.GlobalDisplayName}#{profile.Profile.Data.UserInfo.GlobalDisplayNameCode} profile has been found. Queing for sync");
                
                QueueDBSync.Publish(new MessageDbSync()
                {
                    Task = MessageDbSyncTask.MemberProfile,
                    Data = JsonSerializer.Serialize(profile) // yes, this is redundant.
                });
            }
            profiles.AddOrUpdate(entity, profile, (k, v) => profile);
        }

        return profiles;
    }
}