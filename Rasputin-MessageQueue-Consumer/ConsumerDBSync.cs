using System.Collections.Concurrent;
using System.Text.Json;
using Destiny.Models.Responses;
using Destiny.Models.Schemas;
using Microsoft.EntityFrameworkCore;
using Rasputin.Database;
using Rasputin.Database.Models;
using Rasputin.MessageQueue.Enums;
using Rasputin.MessageQueue.Models;

namespace Rasputin.MessageQueue.Consumer;

public static class ConsumerDbSync
{

    public const int CHUNK_SIZE_ACTIVITIES = 1024;
    
    public static async Task<bool> Process(MessageDbSync message)
    {
        var result = false;
       
        switch (message.Task)
        {
            case MessageDbSyncTask.MemberProfile:
                result = await ProcessProfile(message.Data);
                break;
            case MessageDbSyncTask.ActivityHistory:
                var instanceIds = await ProcessActivityHistory(message.Data, message.Headers);
                result = instanceIds.Length > 0;
                break;
            case MessageDbSyncTask.ActivityStats:
                result = await ProcessActivityStats(message.Data, message.Headers);
                break;
            default:
                LoggerGlobal.Write($"Unsupported db task type: {message.Task}");
                break;
        }
    
        
        return result;
    }

    private static async Task<bool> ProcessActivityStats(string data, Dictionary<string,string> headers)
    {
        var history = JsonSerializer.Deserialize<DestinyHistoricalStatsPeriodGroup[]>(data);
        if (history == null)
        {
            LoggerGlobal.Write($"Failed to deserialize incoming activity stat history data: {data}");
            return false;
        }
        
        return true;
    }

    private static async Task<long[]> ProcessActivityHistory(string data, Dictionary<string,string> headers)
    {

        var history = JsonSerializer.Deserialize<DestinyHistoricalStatsPeriodGroup[]>(data);
        if (history == null)
        {
            LoggerGlobal.Write($"Failed to deserialize incoming activity history data: {data}");
            return [];
        }

        headers.TryGetValue("membership", out var membershipIdRaw);
        headers.TryGetValue("platform", out var platformIdRaw);
        headers.TryGetValue("character", out var characterIdRaw);


        long.TryParse(membershipIdRaw, out var membershipId);
        int.TryParse(platformIdRaw, out var membershipType);
        long.TryParse(characterIdRaw, out var characterId);
            
        var tempHash = new HashSet<long>();
        var historyRecords = new List<MemberActivity>();
        
        LoggerGlobal.Write($"Processing activity history for membership: {membershipId} and character {characterId}");
        foreach (var historyEntry in history)
        {
            tempHash.Add(historyEntry.Details.InstanceId);
            
            historyRecords.Add(new MemberActivity()
            {
                MembershipId = membershipId, 
                CharacterId = characterId, 
                InstanceId = historyEntry.Details.InstanceId, 
                ActivityHash = historyEntry.Details.ReferenceId,
                ActivityHashDirector = historyEntry.Details.DirectorActivityHash, 
                Mode = (long)historyEntry.Details.Mode,
                Modes = string.Join(",",historyEntry.Details.Modes),
                PlatformPlayed = (int)historyEntry.Details.MembershipType,
                Private = historyEntry.Details.IsPrivate,
                OccurredAt = ((DateTimeOffset)historyEntry.Period).ToUnixTimeSeconds(),
                CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                UpdatedAt = 0, 
                DeletedAt = 0
            });
        }
        var instanceIds = tempHash.ToArray();

        // due to the amount of possible history data coming in... 
        // chunk this accordingly
        LoggerGlobal.Write($"Generating chunks for activity history for membership: {membershipId} and character {characterId}");
        
        
        await using (var db = await RasputinDatabase.Connect())
        {
            var chunkCount = 0;
            foreach (var chunk in historyRecords.Chunk(CHUNK_SIZE_ACTIVITIES))
            {
                
                LoggerGlobal.Write($"Writing chunk {++chunkCount} for {membershipId} and character {characterId}");
                await db.MemberActivities.UpsertRange(chunk)
                    .On(p => new { p.MembershipId, p.CharacterId, p.InstanceId, })
                    .WhenMatched((old, @new) => new MemberActivity()
                    {
                        PlatformPlayed = @new.PlatformPlayed, 
                        ActivityHash = @new.ActivityHash, 
                        ActivityHashDirector = @new.ActivityHashDirector, 
                        Mode = @new.Mode, 
                        Modes = @new.Modes, 
                        OccurredAt = @new.OccurredAt, 
                        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                        DeletedAt = 0
                    })
                    .RunAsync();
                LoggerGlobal.Write($"Done Writing chunk {chunkCount} for {membershipId} and character {characterId}");

            }
        }

        return instanceIds;
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
        
        await Task.WhenAll(tasks).ConfigureAwait(false);
        
        return true;
    }

    private static async Task<bool> ProcessTriumphComponent(long membershipId,
        ConcurrentDictionary<string, DestinyRecordComponent> records)
    {
        await using (var db = await RasputinDatabase.Connect())
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
        await using (var db = await RasputinDatabase.Connect())
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
        await using (var db = await RasputinDatabase.Connect())
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