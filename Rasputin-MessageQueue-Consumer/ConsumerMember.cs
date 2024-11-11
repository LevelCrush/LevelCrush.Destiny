using System.Collections.Concurrent;
using System.Text.Json;
using Destiny.Api;
using Destiny.Models.Enums;
using Destiny.Models.Responses;
using Destiny.Models.Schemas;
using Microsoft.EntityFrameworkCore;
using Rasputin.Database;
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

    private static async Task<bool> TaskActivities(string[] entities, bool fresh = false)
    {
        /*
         * When processing an activity message , the entities themselves are membership ids
         * This means we can query them for Info at the same time and get **alot** more value of this message
         * Making activity messages the most efficient way of crawling a member directly
         * 
         */
        var profiles = await TaskInfo(entities);
        
        foreach (var entity in entities)
        {
            var found = profiles.TryGetValue(entity, out var profile);
            if (found && profile != null 
                      && profile.Profile != null 
                      && profile.Profile.Data != null
                      && profile.Characters != null 
                      && profile.Characters.Data != null)
            {
                LoggerGlobal.Write($"Found profile for {entity}. Beginning to crawl characters");
                await Parallel.ForEachAsync(profile.Characters.Data.Values, 
                    async (character, token) => 
                        await TaskActivitiesCharacter(character.MembershipId, character.MembershipType, character.CharacterId, fresh)
                        );

            }
            else
            {
                LoggerGlobal.Write($"Profile could not be found for {entity}");
            }
        }

        return true;
    }

    private static async Task<long> TaskActivitiesCharacter(long membershipId, BungieMembershipType membershipType,
        long characterId, bool fresh = false)
    {
        long startTimestamp = 0;
        await using (var db = await RasputinDatabase.Connect())
        {
            // if fresh, we always start at 0 
            // if not fresh, query the database to see if we can pull the most recent activity (if any)
            startTimestamp = fresh switch
            {
                true => 0,
                false => await db.MemberActivities
                    .Where(x => x.CharacterId == characterId)
                    .Select(p => p.OccurredAt)
                    .DefaultIfEmpty(0)
                    .MaxAsync()
            };
        }

        LoggerGlobal.Write($"Starting to crawl character {characterId} activities using timestamp {startTimestamp} as base");
        var activityHistory = await DestinyActivity.ForCharacter(membershipId, membershipType, characterId, startTimestamp);
        var payload = JsonSerializer.Serialize(activityHistory);
        
        LoggerGlobal.Write($"Done crawling character {characterId} activities. Publishing activity and stats");
            
        // message dedicated to activity history
        QueueDBSync.Publish(new MessageDBSync()
        {
            Task = MessageDBSyncTask.ActivityHistory,
            Data = payload
        });
        
        LoggerGlobal.Write($"Done publishing activity for {characterId}");

        // message dedicated to activity stats
        QueueDBSync.Publish(new MessageDBSync()
        {
            Task = MessageDBSyncTask.ActivityStats,
            Data = payload
        });
        
        LoggerGlobal.Write($"Done publishing stats for {characterId}");
        
        return activityHistory.LongLength;
    }
    

    private static async Task<ConcurrentDictionary<string, DestinyProfileResponse?>> TaskInfo(string[] entities)
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
                QueueDBSync.Publish(new MessageDBSync()
                {
                    Task = MessageDBSyncTask.MemberProfile,
                    Data = JsonSerializer.Serialize(profile) // yes, this is redundant.
                });
            }
            profiles.AddOrUpdate(entity, profile, (k, v) => profile);
        }

        return profiles;
    }
}