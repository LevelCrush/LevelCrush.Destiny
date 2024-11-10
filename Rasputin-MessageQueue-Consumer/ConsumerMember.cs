using System.Text.Json;
using Destiny.Api;
using Destiny.Models.Responses;
using Destiny.Models.Schemas;
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
            default:
                LoggerGlobal.Write($"Unknown member task {message.Task}");
                break;
        }
        
        
        
        return false;
    }

    private static async Task<Dictionary<string, bool>> TaskInfo(string[] entities)
    {
        var result = new Dictionary<string, bool>();

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
        }

        return result;
    }
}