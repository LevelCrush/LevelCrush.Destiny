using System.Collections.Concurrent;
using System.Text.Json;
using Destiny.Models.Responses;
using Destiny.Models.Schemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using Rasputin.Database;
using Rasputin.Database.Models;
using Rasputin.MessageQueue.Enums;
using Rasputin.MessageQueue.Models;

namespace Rasputin.MessageQueue.Consumer;

public static class ConsumerDBSync
{
    public static async Task<bool> Process(MessageDBSync message)
    {
        var result = false;
       
        switch (message.Task)
        {
            case MessageDBSyncTask.MemberProfile:
                result = await ProcessProfile(message.Data);
                break;
            default:
                LoggerGlobal.Write($"Unsupported db task type: {message.Task}");
                break;
        }
    
        
        return result;
    }

    private static async Task<bool> ProcessProfile(string data)
    {
        var profileResponse = JsonSerializer.Deserialize<DestinyProfileResponse>(data);
        long membershipId = 0;
        if (profileResponse == null)
        {
            LoggerGlobal.Write($"Failed to deserialize as DestinyProfileResponse:\r\n{data}");
            return false;
        }
        
        var tasks = new List<Task>();
        if (profileResponse.Profile != null && profileResponse.Profile.Data != null)
        {
            membershipId = profileResponse.Profile.Data.UserInfo.MembershipId;
            tasks.Add(ProcessProfileComponent(profileResponse.Profile.Data));
        }

        if (profileResponse.Characters != null && profileResponse.Characters.Data != null)
        {
            var characters = profileResponse.Characters.Data.Values.ToArray();
            tasks.Add(ProcessCharacterComponentMultiple(characters));
        }

        if (profileResponse.ProfileRecords != null && profileResponse.ProfileRecords.Data != null)
        {
            tasks.Add(ProcessTriumphComponent(membershipId, profileResponse.ProfileRecords.Data.Records));
        }
        
       await Task.WhenAll(tasks);
        
        // save any remaining changes if there are
       // await db.SaveChangesAsync();
        
        
        return true;
    }

    private static async Task<bool> ProcessTriumphComponent(long membershipId,
        ConcurrentDictionary<string, DestinyRecordComponent> records)
    {
        using (var db = await RasputinDatabase.Connect())
        {
            LoggerGlobal.Write($"Saving {membershipId} triumph records. Total of {records.Count}");

            MemberTriumph[] triumphs = new MemberTriumph[records.Count];
            var i = 0;
            foreach (var (hash, triumph) in records)
            {
                uint triumphHash = 0;
                var converted = uint.TryParse(hash, out triumphHash);
                triumphs[i++] = new MemberTriumph()
                {
                    MembershipId = membershipId,
                    Hash = converted ? triumphHash : 0,
                    State = triumph.State,
                    TimesCompleted = triumph.CompletedCount,
                    CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    UpdatedAt = 0,
                    DeletedAt = 0
                };
            }

            await db.MemberTriumphs.UpsertRange(triumphs)
                .On(x => new { x.MembershipId, x.Hash })
                .WhenMatched((old, @new) => new MemberTriumph()
                {
                    State = @new.State,
                    TimesCompleted = @new.TimesCompleted,
                    UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                })
                .RunAsync();
        }

        return true;
    }

    private static async Task<bool> ProcessProfileComponent(DestinyProfileComponent profile)
    {
        using (var db = await RasputinDatabase.Connect())
        {
            var user = profile.UserInfo;
            var membershipId = user.MembershipId;
            var membershipType = user.MembershipType;
            var globalDisplayName = $"{user.GlobalDisplayName}#{user.GlobalDisplayNameCode.ToString().PadLeft(4, '0')}";

            LoggerGlobal.Write($"Syncing Profile for ({user.DisplayName} | {globalDisplayName}");

            await db.Members.Upsert(new Member()
                {
                    MembershipId = membershipId,
                    Platform = (int)membershipType,
                    DisplayName = user.DisplayName,
                    DisplayNameGlobal = globalDisplayName,
                    GuardianRankCurrent = profile.GuardianRankCurrent,
                    GuardianRankLifetime = profile.GuardianRankLifetime,
                    LastPlayedAt = ((DateTimeOffset)profile.DateLastPlayed).ToUnixTimeSeconds(),
                    CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    UpdatedAt = 0,
                    DeletedAt = 0
                }).On(v => new { v.MembershipId })
                .WhenMatched((old, @new) => new Member()
                {
                    Platform = @new.Platform,
                    DisplayName = @new.DisplayName,
                    DisplayNameGlobal = @new.DisplayNameGlobal,
                    GuardianRankCurrent = @new.GuardianRankCurrent,
                    GuardianRankLifetime = @new.GuardianRankLifetime,
                    LastPlayedAt = @new.LastPlayedAt,
                    UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                }).RunAsync();

            LoggerGlobal.Write($"Done syncing profile for ({user.DisplayName} | {globalDisplayName}");
        }

        return true;
       
    }

    private static async Task<bool> ProcessCharacterComponent(DestinyCharacterComponent character)
    {

        using (var db = await RasputinDatabase.Connect())
        {
            LoggerGlobal.Write($"Syncing character: {character.CharacterId} tied to member {character.MembershipId}");


            await db.MemberCharacters.Upsert(new MemberCharacter()
                {
                    MembershipId = character.MembershipId,
                    CharacterId = character.CharacterId,
                    Platform = (int)character.MembershipType,
                    ClassHash = character.ClassHash,
                    Light = character.Light,
                    LastPlayedAt = ((DateTimeOffset)character.LastPlayed).ToUnixTimeSeconds(),
                    EmblemHash = character.EmblemHash,
                    EmblemUrl = character.EmblemPath,
                    EmblemBackgroundUrl = character.EmblemBackgroundPath,
                    MinutesPlayedLifetime = character.MinutesPlayedLifeTime,
                    MinutesPlayedSession = character.MinutesPlayedSession,
                    CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    UpdatedAt = 0,
                    DeletedAt = 0
                }).On(v => new { v.MembershipId, v.CharacterId })
                .WhenMatched((old, @new) => new MemberCharacter()
                {
                    CharacterId = @new.CharacterId,
                    Platform = @new.Platform,
                    ClassHash = @new.ClassHash,
                    Light = @new.Light,
                    LastPlayedAt = @new.LastPlayedAt,
                    EmblemHash = @new.EmblemHash,
                    EmblemUrl = @new.EmblemUrl,
                    EmblemBackgroundUrl = @new.EmblemBackgroundUrl,
                    MinutesPlayedLifetime = @new.MinutesPlayedLifetime,
                    MinutesPlayedSession = @new.MinutesPlayedSession,
                    UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                }).RunAsync();
        }

        return true;
    }

    private static async Task<bool> ProcessCharacterComponentMultiple(DestinyCharacterComponent[] characters)
    {
        
        await Parallel.ForEachAsync(
            characters, 
            async (character,token) => await ProcessCharacterComponent(character)
            );
                    
        return true;
    }

}