using System.Collections.Concurrent;
using System.Text.Json;
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

public static class ConsumerDbSync
{
    public const int ChunkSizeActivities = 1000;
    public const int ChunkSizeStats = 1000;
    public const int ChunkSizeInstances = 100;
    public const int ChunkSizeInstanceMembers = 1000;
    
    public static async Task<bool> Process(MessageDbSync message)
    {
        var result = false;
        long[]? instanceIds = null;
        switch (message.Task)
        {
            case MessageDbSyncTask.MemberProfile:
                result = await ProcessProfile(message.Data);
                break;
            case MessageDbSyncTask.ActivityHistory:
                instanceIds = await ProcessActivityHistory(message.Data, message.Headers);
                result = instanceIds.Length > 0;
                break;
            case MessageDbSyncTask.ActivityStats:
                instanceIds = await ProcessActivityStats(message.Data, message.Headers);
                result = instanceIds.Length > 0;
                break;
            case MessageDbSyncTask.Instance:
                result = await ProcessInstance(message.Data);
                break;
            default:
                LoggerGlobal.Write($"Unsupported db task type: {message.Task}");
                break;
        }
        
        // if we have gathered any instance ids push them into our instance queue
        // for now, we will push **each** instance into the queue instead of taking advantage of the batching
        // in theory, more consumers = better paralleism that is reliable when querying the bungie api 
        // provided each consumer is on a different machine with a different ip
        // best case we can increase this number to chunk 100 or so at a time
        if (instanceIds != null)
        {
            LoggerGlobal.Write($"Sending {instanceIds.Length} instance ids to message queue");
            foreach (var chunk in instanceIds.Chunk(ChunkSizeInstances))
            {
                string[] instanceStringIds = new string[chunk.Length];
                var i = 0;
                foreach (var instanceId in chunk)
                {
                    instanceStringIds[i++] = instanceId.ToString();
                }
                
                QueueInstance.Publish(new MessageInstance()
                {
                    Entities = instanceStringIds
                });
            }
            LoggerGlobal.Write($"Done sending {instanceIds.Length} instance ids to message queue");
        }


        return result;
    }

    private static async Task<bool> ProcessInstance(string messageData)
    {
        var instanceMemberRecords = new List<InstanceMember>();

        var carnageReport = JsonSerializer.Deserialize<DestinyPostGameCarnageReportData>(messageData);
        if (carnageReport == null)
        {
            LoggerGlobal.Write($"Failed to deserialize carnage report: {messageData}");
            return false;
        }
        
        var instanceId = carnageReport.ActivityDetails.InstanceId;
        var activityCompleted = false;
        var completionReasons = new ConcurrentDictionary<string, bool>();
        var instanceMemberMap = new ConcurrentDictionary<(long,BungieMembershipType), List<long>>();
        foreach (var data in carnageReport.Entries)
        {
            var playerMembershipId = data.Player.User.MembershipId;
            var characterId = data.CharacterId;
            var membershipType = data.Player.User.MembershipType;

            data.Values.TryGetValue("completed", out var completedStatValue);
            var playerCompleted = false;
            if (completedStatValue != null)
            {
                playerCompleted = completedStatValue.Basic.DisplayValue == "Yes";
            }
            
            data.Values.TryGetValue("completionReason", out var completionReasonValue);
            var completionReason = "";
            if (completedStatValue != null)
            {
                completionReason = completedStatValue.Basic.DisplayValue;
            }
            
            
            // insert instanceMember Record 
            instanceMemberRecords.Add(new InstanceMember()
            {
                InstanceId = instanceId,
                MembershipId = playerMembershipId,
                CharacterId = characterId,
                Platform = (int)membershipType,
                ClassName = data.Player.CharacterClass,
                ClassHash = data.Player.ClassHash, 
                LightLevel = data.Player.LightLevel,
                ClanName = data.Player.ClanName,
                ClanTag = data.Player.ClanTag,
                Completed = playerCompleted,
                CompletionReason = completionReason,
                CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                UpdatedAt = 0,
                DeletedAt = 0
            });
            

            // so long as one player has completed, the entire instance can be marked as completed
            if (playerCompleted)
            {
                activityCompleted = true;
            }
            
            // we only need to add to the dictionary the first time
            // track to se if we have completed. Note, we only care about the first "completion" reason for whatever comes first
            // for the purpose of this sync we don't need to know **every** player reason
            // that is already handled by character activity history sync
            completionReasons.TryAdd(completionReason, playerCompleted);
            
            instanceMemberMap.AddOrUpdate(
                (playerMembershipId, membershipType), // tuple as key
                [characterId], // initialize list
                (key, oldValue) =>
            {
                // just append onto our current list here
                oldValue.Add(characterId);
                return oldValue;
            });
        }

        var reasons = string.Join(",", completionReasons.Keys.ToArray());

        var instanceRecord = new Instance()
        {
            InstanceId = instanceId,
            OccurredAt = ((DateTimeOffset)carnageReport.Period).ToUnixTimeSeconds(),
            StartingPhaseIndex = carnageReport.StartingPhaseIndex ?? 0,
            StartedFromBeginning = carnageReport.ActivityWasStartedFromBeginning ?? true,
            ActivityHash = carnageReport.ActivityDetails.ReferenceId,
            ActivityDirectorHash = carnageReport.ActivityDetails.DirectorActivityHash,
            IsPrivate = carnageReport.ActivityDetails.IsPrivate,
            Completed = activityCompleted,
            CompletionReasons = reasons,
            CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            UpdatedAt = 0,
            DeletedAt = 0
        };

        await using (var db = await RasputinDatabase.Connect())
        {
            LoggerGlobal.Write($"Writing {instanceRecord} to database");
            await db.Instances.Upsert(instanceRecord)
                .On(p => new { p.InstanceId })
                .WhenMatched((@old, @new) => new Instance()
                {
                    OccurredAt = @new.OccurredAt,
                    StartedFromBeginning = @new.StartedFromBeginning,
                    StartingPhaseIndex = @new.StartingPhaseIndex,
                    ActivityHash = @new.ActivityHash,
                    ActivityDirectorHash = @new.ActivityDirectorHash,
                    IsPrivate = @new.IsPrivate,
                    Completed = @new.Completed,
                    CompletionReasons = @new.CompletionReasons,
                    UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    DeletedAt = 0
                })
                .RunAsync();
            
            
            LoggerGlobal.Write($"Done writing {instanceRecord} to database");
            LoggerGlobal.Write($"Now writing {instanceMemberRecords.Count} to database tied to {instanceRecord}");
            
            foreach(var chunk in instanceMemberRecords.Chunk(ChunkSizeInstanceMembers)) {
                await db.InstanceMembers.UpsertRange(instanceMemberRecords)
                    .On(p => new { p.MembershipId, p.CharacterId, p.InstanceId })
                    .WhenMatched((@old, @new) => new InstanceMember()
                    {
                        ClassHash = @new.ClassHash,
                        ClassName = @new.ClassName,
                        EmblemHash = @new.EmblemHash,
                        LightLevel = @new.LightLevel,
                        ClanName = @new.ClanName,
                        ClanTag = @new.ClanTag,
                        Completed = @new.Completed,
                        CompletionReason = @new.CompletionReason,
                        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                        DeletedAt = 0
                    })
                    .RunAsync();

            }
        }
        
        return true;
    }

    private static async Task<long[]> ProcessActivityStats(string data, Dictionary<string,string> headers)
    {
        var history = JsonSerializer.Deserialize<DestinyHistoricalStatsPeriodGroup[]>(data);
        if (history == null)
        {
            LoggerGlobal.Write($"Failed to deserialize incoming activity stat history data: {data}");
            return [];
        }
        
        headers.TryGetValue("membership", out var membershipIdRaw);
        headers.TryGetValue("platform", out var platformIdRaw);
        headers.TryGetValue("character", out var characterIdRaw);


        long.TryParse(membershipIdRaw, out var membershipId);
        int.TryParse(platformIdRaw, out var membershipType);
        long.TryParse(characterIdRaw, out var characterId);
            
        var tempHash = new HashSet<long>();
        var records = new List<MemberActivityStat>();
        
        LoggerGlobal.Write($"Processing activity history stats for membership: {membershipId} and character {characterId}");
        foreach (var historyEntry in history)
        {
            tempHash.Add(historyEntry.Details.InstanceId);

            foreach (var (id, stat) in historyEntry.Values)
            {
                records.Add(new MemberActivityStat()
                {
                    MembershipId = membershipId,
                    CharacterId = characterId,
                    InstanceId = historyEntry.Details.InstanceId,
                    Name = stat.StatId,
                    Value = stat.Basic.Value,
                    ValueDisplay = stat.Basic.DisplayValue,
                    CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    UpdatedAt = 0,
                    DeletedAt = 0
                });
            }
        }
        var instanceIds = tempHash.ToArray();
        
        LoggerGlobal.Write($"Preparing to write {records.Count} stat records for membership: {membershipId} and character {characterId}");

        await using (var db = await RasputinDatabase.Connect())
        {
            var chunkCount = 0;
            foreach (var chunk in records.Chunk(ChunkSizeStats))
            {
                LoggerGlobal.Write($"Writing stat chunk {++chunkCount} for {membershipId} and character {characterId}");
                await db.MemberActivityStats.UpsertRange(chunk)
                    .On(p => new { p.MembershipId, p.CharacterId, p.InstanceId, p.Name})
                    .WhenMatched((@old, @new) => new MemberActivityStat()
                    {
                        Value = @new.Value, 
                        ValueDisplay = @new.ValueDisplay, 
                        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                        DeletedAt = 0
                    })
                    .RunAsync();
                LoggerGlobal.Write($"Done Writing stat chunk {chunkCount} for {membershipId} and character {characterId}");
            }
        }

        return instanceIds;
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
            foreach (var chunk in historyRecords.Chunk(ChunkSizeActivities))
            {
                
                LoggerGlobal.Write($"Writing history chunk {++chunkCount} for {membershipId} and character {characterId}");
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
                LoggerGlobal.Write($"Done Writing history chunk {chunkCount} for {membershipId} and character {characterId}");

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