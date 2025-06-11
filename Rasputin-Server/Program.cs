using Destiny;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rasputin_Redis;
using Rasputin_Server.Model;
using Rasputin.Database;
using Rasputin.Database.Models;
using System.Text.Json;

var destinyApiConfig = DestinyConfig.Load();
BungieClient.ApiKey = destinyApiConfig.ApiKey;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Rasputin API", Version = "v1" });
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// Helper methods
async Task<T?> ExecuteDbQuery<T>(Func<DBDestinyContext, Task<T>> query)
{
    try
    {
        await using var db = await RasputinDatabase.Connect();
        return await query(db);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Database query failed");
        return default;
    }
}

// Helper to find member by identifier (MembershipId or DisplayNameGlobal)
async Task<Member?> FindMemberByIdentifier(DBDestinyContext db, string identifier)
{
    // Try to parse as long for membership ID
    if (long.TryParse(identifier, out var membershipId))
    {
        // First try by membership ID
        var memberById = await db.Members
            .FirstOrDefaultAsync(m => m.MembershipId == membershipId);
        if (memberById != null) return memberById;
    }
    
    // Try by display name (case-insensitive)
    return await db.Members
        .FirstOrDefaultAsync(m => m.DisplayNameGlobal == identifier);
}

// ========== MEMBER ENDPOINTS ==========

// Get member summary by Bungie name or membership ID
app.MapGet("/api/members/{identifier}", async (string identifier) =>
{
    var result = await ExecuteDbQuery(async db =>
    {
        // Try to parse as long for membership ID
        long membershipId = 0;
        bool isNumeric = long.TryParse(identifier, out membershipId);

        var member = await db.Members
            .FirstOrDefaultAsync(m => 
                (isNumeric && m.MembershipId == membershipId) || 
                m.DisplayNameGlobal == identifier ||
                m.DisplayName == identifier);

        if (member == null) return null;

        // Get characters
        var characters = await db.MemberCharacters
            .Where(mc => mc.MembershipId == member.MembershipId)
            .ToListAsync();

        // Get clan memberships
        var clanMemberships = await db.ClanMembers
            .Where(cm => cm.MembershipId == member.MembershipId)
            .Join(db.Clans,
                cm => cm.GroupId,
                c => c.GroupId,
                (cm, c) => new { ClanMember = cm, Clan = c })
            .ToListAsync();

        return new MemberSummaryResponse
        {
            MembershipId = member.MembershipId.ToString(),
            DisplayName = member.DisplayName ?? "",
            DisplayNameGlobal = member.DisplayNameGlobal ?? "",
            Platform = member.Platform,
            GuardianRankCurrent = member.GuardianRankCurrent,
            GuardianRankLifetime = member.GuardianRankLifetime,
            LastPlayedAt = DateTimeOffset.FromUnixTimeSeconds(member.LastPlayedAt).UtcDateTime,
            Characters = characters.Select(c => new CharacterSummary
            {
                CharacterId = c.CharacterId.ToString(),
                ClassHash = c.ClassHash.ToString(),
                ClassName = GetClassName(c.ClassHash.ToString()),
                LightLevel = c.Light,
                MinutesPlayedSession = c.MinutesPlayedSession,
                MinutesPlayedLifetime = c.MinutesPlayedLifetime,
                LastPlayedAt = c.LastPlayedAt > 0 ? DateTimeOffset.FromUnixTimeSeconds(c.LastPlayedAt).UtcDateTime : null,
                EmblemUrl = c.EmblemUrl
            }).ToList(),
            Clans = clanMemberships.Select(cm => new ClanMembershipSummary
            {
                GroupId = cm.ClanMember.GroupId.ToString(),
                ClanName = cm.Clan?.Name ?? "",
                ClanTag = cm.Clan?.CallSign,
                GroupRole = cm.ClanMember.GroupRole,
                JoinedAt = cm.ClanMember.JoinedAt > 0 ? DateTimeOffset.FromUnixTimeSeconds(cm.ClanMember.JoinedAt).UtcDateTime : null
            }).ToList()
        };
    });

    return result != null 
        ? Results.Ok(ApiResponse<MemberSummaryResponse>.Ok(result))
        : Results.NotFound(ApiResponse<MemberSummaryResponse>.Fail("Member not found"));
});

// Get member's recent activities
app.MapGet("/api/members/{identifier}/activities", async (
    string identifier,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 20,
    [FromQuery] int? mode = null,
    [FromQuery] DateTime? startDate = null,
    [FromQuery] DateTime? endDate = null) =>
{
    var result = await ExecuteDbQuery(async db =>
    {
        var member = await FindMemberByIdentifier(db, identifier);
        if (member == null) return null;

        var query = db.MemberActivities
            .Where(ma => ma.MembershipId == member.MembershipId);

        if (mode.HasValue)
            query = query.Where(ma => ma.Mode == mode.Value);

        if (startDate.HasValue)
        {
            var startTimestamp = new DateTimeOffset(startDate.Value).ToUnixTimeSeconds();
            query = query.Where(ma => ma.OccurredAt >= startTimestamp);
        }

        if (endDate.HasValue)
        {
            var endTimestamp = new DateTimeOffset(endDate.Value).ToUnixTimeSeconds();
            query = query.Where(ma => ma.OccurredAt <= endTimestamp);
        }

        var totalCount = await query.CountAsync();
        
        var activities = await query
            .OrderByDescending(ma => ma.OccurredAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Get activity stats for these activities
        var instanceIds = activities.Select(a => a.InstanceId).Distinct().ToList();
        var stats = await db.MemberActivityStats
            .Where(mas => mas.MembershipId == member.MembershipId && instanceIds.Contains(mas.InstanceId))
            .ToListAsync();

        // Get instance completion status
        var instances = await db.Instances
            .Where(i => instanceIds.Contains(i.InstanceId))
            .ToListAsync();

        var activitySummaries = activities.Select(ma => new ActivitySummary
        {
            InstanceId = ma.InstanceId.ToString(),
            ActivityHash = ma.ActivityHash.ToString(),
            Mode = (int)ma.Mode,
            Completed = instances.FirstOrDefault(i => i.InstanceId == ma.InstanceId)?.Completed ?? false,
            OccurredAt = DateTimeOffset.FromUnixTimeSeconds(ma.OccurredAt).UtcDateTime,
            Stats = stats
                .Where(s => s.InstanceId == ma.InstanceId)
                .ToDictionary(s => s.Name ?? "", s => s.Value)
        }).ToList();

        return new PagedResponse<ActivitySummary>
        {
            Items = activitySummaries,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    });

    return result != null 
        ? Results.Ok(ApiResponse<PagedResponse<ActivitySummary>>.Ok(result))
        : Results.Problem("Failed to retrieve activities");
});

// Get member's activity statistics
app.MapGet("/api/members/{identifier}/stats", async (
    string identifier,
    [FromQuery] DateTime? startDate = null,
    [FromQuery] DateTime? endDate = null) =>
{
    var result = await ExecuteDbQuery(async db =>
    {
        var member = await FindMemberByIdentifier(db, identifier);
        if (member == null) return null;

        var query = db.MemberActivities
            .Where(ma => ma.MembershipId == member.MembershipId);

        if (startDate.HasValue)
        {
            var startTimestamp = new DateTimeOffset(startDate.Value).ToUnixTimeSeconds();
            query = query.Where(ma => ma.OccurredAt >= startTimestamp);
        }

        if (endDate.HasValue)
        {
            var endTimestamp = new DateTimeOffset(endDate.Value).ToUnixTimeSeconds();
            query = query.Where(ma => ma.OccurredAt <= endTimestamp);
        }

        var activities = await query.ToListAsync();

        if (!activities.Any()) return null;

        // Get all stats for these activities
        var instanceIds = activities.Select(a => a.InstanceId).Distinct().ToList();
        var stats = await db.MemberActivityStats
            .Where(mas => mas.MembershipId == member.MembershipId && instanceIds.Contains(mas.InstanceId))
            .ToListAsync();

        var memberStats = new MemberActivityStats
        {
            MembershipId = member.MembershipId.ToString(),
            TotalActivities = activities.Count,
            FirstActivity = DateTimeOffset.FromUnixTimeSeconds(activities.Min(a => a.OccurredAt)).UtcDateTime,
            LastActivity = DateTimeOffset.FromUnixTimeSeconds(activities.Max(a => a.OccurredAt)).UtcDateTime,
            ActivitiesByMode = activities
                .GroupBy(a => GetModeName((int)a.Mode))
                .ToDictionary(g => g.Key, g => g.Count()),
            AverageStats = stats
                .GroupBy(s => s.Name ?? "")
                .Where(g => !string.IsNullOrEmpty(g.Key))
                .ToDictionary(
                    g => g.Key,
                    g => g.Average(s => s.Value)
                )
        };

        return memberStats;
    });

    return result != null 
        ? Results.Ok(ApiResponse<MemberActivityStats>.Ok(result))
        : Results.NotFound(ApiResponse<MemberActivityStats>.Fail("No activities found"));
});

// Get member's titles (existing endpoint refactored)
app.MapGet("/api/members/{identifier}/titles", async (string identifier) =>
{
    var result = await ExecuteDbQuery(async db =>
    {
        var res = await db.Database.SqlQuery<DestinyMemberTitleResponse>(@$"
            WITH
            target_members AS (
                SELECT
                    members.*
                FROM clans
                INNER JOIN clan_members ON clans.group_id = clan_members.group_id
                INNER JOIN members ON clan_members.membership_id = members.membership_id
                WHERE clans.is_network = 1
                AND (members.membership_id = {identifier}  OR members.display_name_global LIKE {identifier})
            ),
            triumph_titles AS (
                SELECT
                    triumphs.*
                FROM manifest_triumphs AS triumphs
                WHERE triumphs.is_title = 1
            ),
            titles_earned_account AS (
                SELECT
                    triumph_titles.title,
                    COALESCE(SUM(member_triumphs.state & 64 = 64), 0) AS amount
                FROM target_members
                INNER JOIN member_triumphs ON target_members.membership_id = member_triumphs.membership_id
                INNER JOIN triumph_titles ON member_triumphs.hash = triumph_titles.hash
                GROUP BY triumph_titles.title
            ),
            titles_earned_character AS (
                SELECT
                    member_character_triumphs.membership_id,
                    COUNT(DISTINCT member_character_triumphs.character_id) AS characters,
                    triumph_titles.title,
                    COALESCE(SUM(member_character_triumphs.state & 64 = 64), 0) AS amount
                FROM target_members
                INNER JOIN member_character_triumphs ON target_members.membership_id = member_character_triumphs.membership_id
                INNER JOIN triumph_titles ON member_character_triumphs.hash = triumph_titles.hash
                GROUP BY triumph_titles.title, member_character_triumphs.membership_id
            ),
            titles_earned_character_member AS (
                SELECT
                    titles_earned_character.membership_id,
                    titles_earned_character.title,
                    CEIL(titles_earned_character.amount / titles_earned_character.characters) AS amount
                FROM target_members
                INNER JOIN titles_earned_character ON target_members.membership_id = titles_earned_character.membership_id
                GROUP BY titles_earned_character.title, titles_earned_character.membership_id
            ),
            titles_earned_character_merged AS (
                 SELECT
                    titles_earned_character_member.title,
                    COALESCE(SUM(titles_earned_character_member.amount), 0) AS amount
                FROM titles_earned_character_member
                GROUP BY titles_earned_character_member.title
            ),
            titles_earned AS (
                SELECT
                   titles_earned_account.title,
                   titles_earned_account.amount
                FROM titles_earned_account
                UNION
                SELECT
                    titles_earned_character_merged.title,
                    titles_earned_character_merged.amount
                FROM titles_earned_character_merged
            )
            SELECT * FROM titles_earned
            WHERE amount > 0
            ORDER BY title ASC
            ").ToArrayAsync();

        return res;
    });

    return result != null 
        ? Results.Ok(ApiResponse<DestinyMemberTitleResponse[]>.Ok(result))
        : Results.NotFound(ApiResponse<DestinyMemberTitleResponse[]>.Fail("Member not found"));
});

// ========== CLAN ENDPOINTS ==========

// Get clan summary
app.MapGet("/api/clans/{groupId}", async (string groupId) =>
{
    var result = await ExecuteDbQuery(async db =>
    {
        if (!long.TryParse(groupId, out var groupIdLong))
            return null;

        var clan = await db.Clans
            .FirstOrDefaultAsync(c => c.GroupId == groupIdLong);

        if (clan == null) return null;

        var memberCount = await db.ClanMembers
            .Where(cm => cm.GroupId == groupIdLong && cm.DeletedAt == 0)
            .CountAsync();

        return new ClanSummaryResponse
        {
            GroupId = clan.GroupId.ToString(),
            Name = clan.Name ?? "",
            Slug = clan.Slug,
            Motto = clan.Motto,
            About = clan.About,
            CallSign = clan.CallSign,
            IsNetwork = clan.IsNetwork,
            MemberCount = memberCount,
            CreatedAt = clan.CreatedAt > 0 ? DateTimeOffset.FromUnixTimeSeconds(clan.CreatedAt).UtcDateTime : null
        };
    });

    return result != null 
        ? Results.Ok(ApiResponse<ClanSummaryResponse>.Ok(result))
        : Results.NotFound(ApiResponse<ClanSummaryResponse>.Fail("Clan not found"));
});

// Get clan roster
app.MapGet("/api/clans/{groupId}/roster", async (
    string groupId,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 50,
    [FromQuery] string? sortBy = "lastPlayed") =>
{
    var result = await ExecuteDbQuery(async db =>
    {
        if (!long.TryParse(groupId, out var groupIdLong))
            return null;

        var clan = await db.Clans
            .FirstOrDefaultAsync(c => c.GroupId == groupIdLong);

        if (clan == null) return null;

        var clanMembersQuery = db.ClanMembers
            .Where(cm => cm.GroupId == groupIdLong && cm.DeletedAt == 0)
            .Join(db.Members,
                cm => cm.MembershipId,
                m => m.MembershipId,
                (cm, m) => new { ClanMember = cm, Member = m });

        clanMembersQuery = sortBy?.ToLower() switch
        {
            "name" => clanMembersQuery.OrderBy(cm => cm.Member.DisplayNameGlobal),
            "rank" => clanMembersQuery.OrderByDescending(cm => cm.Member.GuardianRankCurrent),
            "joined" => clanMembersQuery.OrderByDescending(cm => cm.ClanMember.JoinedAt),
            _ => clanMembersQuery.OrderByDescending(cm => cm.Member.LastPlayedAt)
        };

        var totalCount = await clanMembersQuery.CountAsync();

        var members = await clanMembersQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(cm => new ClanMemberDetails
            {
                MembershipId = cm.Member.MembershipId.ToString(),
                DisplayName = cm.Member.DisplayName ?? "",
                DisplayNameGlobal = cm.Member.DisplayNameGlobal ?? "",
                Platform = cm.Member.Platform,
                GroupRole = cm.ClanMember.GroupRole,
                JoinedAt = cm.ClanMember.JoinedAt > 0 ? DateTimeOffset.FromUnixTimeSeconds(cm.ClanMember.JoinedAt).UtcDateTime : null,
                LastPlayedAt = cm.Member.LastPlayedAt > 0 ? DateTimeOffset.FromUnixTimeSeconds(cm.Member.LastPlayedAt).UtcDateTime : null,
                GuardianRankCurrent = cm.Member.GuardianRankCurrent
            })
            .ToListAsync();

        return new PagedResponse<ClanMemberDetails>
        {
            Items = members,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    });

    return result != null 
        ? Results.Ok(ApiResponse<PagedResponse<ClanMemberDetails>>.Ok(result))
        : Results.NotFound(ApiResponse<PagedResponse<ClanMemberDetails>>.Fail("Clan not found"));
});

// Get clan activity statistics
app.MapGet("/api/clans/{groupId}/stats", async (
    string groupId,
    [FromQuery] DateTime? startDate = null,
    [FromQuery] DateTime? endDate = null) =>
{
    var result = await ExecuteDbQuery(async db =>
    {
        if (!long.TryParse(groupId, out var groupIdLong))
            return null;

        var clan = await db.Clans.FirstOrDefaultAsync(c => c.GroupId == groupIdLong);
        if (clan == null) return null;

        var memberIds = await db.ClanMembers
            .Where(cm => cm.GroupId == groupIdLong && cm.DeletedAt == 0)
            .Select(cm => cm.MembershipId)
            .ToListAsync();

        var activitiesQuery = db.MemberActivities
            .Where(ma => memberIds.Contains(ma.MembershipId));

        if (startDate.HasValue)
        {
            var startTimestamp = new DateTimeOffset(startDate.Value).ToUnixTimeSeconds();
            activitiesQuery = activitiesQuery.Where(ma => ma.OccurredAt >= startTimestamp);
        }

        if (endDate.HasValue)
        {
            var endTimestamp = new DateTimeOffset(endDate.Value).ToUnixTimeSeconds();
            activitiesQuery = activitiesQuery.Where(ma => ma.OccurredAt <= endTimestamp);
        }

        var activities = await activitiesQuery.ToListAsync();

        if (!activities.Any()) 
        {
            return new ClanActivityStats
            {
                GroupId = groupId,
                ClanName = clan.Name ?? "",
                TotalActivities = 0,
                ActiveMembers = 0,
                ActivitiesByMode = new Dictionary<string, int>()
            };
        }

        var stats = new ClanActivityStats
        {
            GroupId = groupId,
            ClanName = clan.Name ?? "",
            TotalActivities = activities.Count,
            ActiveMembers = activities.Select(a => a.MembershipId).Distinct().Count(),
            EarliestActivity = DateTimeOffset.FromUnixTimeSeconds(activities.Min(a => a.OccurredAt)).UtcDateTime,
            LatestActivity = DateTimeOffset.FromUnixTimeSeconds(activities.Max(a => a.OccurredAt)).UtcDateTime,
            ActivitiesByMode = activities
                .GroupBy(a => GetModeName((int)a.Mode))
                .ToDictionary(g => g.Key, g => g.Count())
        };

        return stats;
    });

    return result != null 
        ? Results.Ok(ApiResponse<ClanActivityStats>.Ok(result))
        : Results.NotFound(ApiResponse<ClanActivityStats>.Fail("Clan not found"));
});

// Get all network clans
app.MapGet("/api/clans", async ([FromQuery] bool? isNetwork = null) =>
{
    var result = await ExecuteDbQuery(async db =>
    {
        var query = db.Clans.AsQueryable();
        
        if (isNetwork.HasValue)
            query = query.Where(c => c.IsNetwork == isNetwork.Value);

        var clans = await query.ToListAsync();

        // Get member counts
        var clanIds = clans.Select(c => c.GroupId).ToList();
        var memberCounts = await db.ClanMembers
            .Where(cm => clanIds.Contains(cm.GroupId) && cm.DeletedAt == 0)
            .GroupBy(cm => cm.GroupId)
            .Select(g => new { GroupId = g.Key, Count = g.Count() })
            .ToListAsync();

        var clanSummaries = clans.Select(c => new ClanSummaryResponse
        {
            GroupId = c.GroupId.ToString(),
            Name = c.Name ?? "",
            Slug = c.Slug,
            Motto = c.Motto,
            CallSign = c.CallSign,
            IsNetwork = c.IsNetwork,
            MemberCount = memberCounts.FirstOrDefault(mc => mc.GroupId == c.GroupId)?.Count ?? 0
        })
        .OrderBy(c => c.Name)
        .ToList();

        return clanSummaries;
    });

    return Results.Ok(ApiResponse<List<ClanSummaryResponse>>.Ok(result ?? new List<ClanSummaryResponse>()));
});

// ========== LEADERBOARD ENDPOINTS ==========

// Get leaderboard for specific stat
app.MapGet("/api/leaderboards/{stat}", async (
    string stat,
    [FromQuery] string period = "all",
    [FromQuery] int limit = 100,
    [FromQuery] bool networkOnly = true) =>
{
    var result = await ExecuteDbQuery(async db =>
    {
        var (startDate, endDate) = GetPeriodDates(period);

        // This is a simplified example - you'd want to customize based on the stat type
        var leaderboardData = await db.Database.SqlQuery<LeaderboardEntry>($@"
            WITH network_members AS (
                SELECT DISTINCT m.membership_id, m.display_name_global, cm.clan_name, cm.clan_tag
                FROM members m
                INNER JOIN clan_members cm ON m.membership_id = cm.membership_id
                INNER JOIN clans c ON cm.group_id = c.group_id
                WHERE ({networkOnly} = 0 OR c.is_network = 1)
                AND cm.deleted_at = 0
            ),
            stat_aggregates AS (
                SELECT 
                    nm.membership_id,
                    nm.display_name_global,
                    nm.clan_name,
                    nm.clan_tag,
                    SUM(mas.value) as total_value
                FROM network_members nm
                INNER JOIN member_activity_stats mas ON nm.membership_id = mas.membership_id
                INNER JOIN member_activities ma ON mas.membership_id = ma.membership_id 
                    AND mas.instance_id = ma.instance_id
                WHERE mas.name = {stat}
                AND ({startDate} IS NULL OR ma.occurred_at >= {startDate})
                AND ({endDate} IS NULL OR ma.occurred_at <= {endDate})
                GROUP BY nm.membership_id, nm.display_name_global, nm.clan_name, nm.clan_tag
            )
            SELECT 
                ROW_NUMBER() OVER (ORDER BY total_value DESC) as Rank,
                membership_id as MembershipId,
                display_name_global as DisplayName,
                clan_name as ClanName,
                clan_tag as ClanTag,
                total_value as Value
            FROM stat_aggregates
            ORDER BY total_value DESC
            LIMIT {limit}
        ").ToListAsync();

        return new LeaderboardResponse
        {
            LeaderboardType = stat,
            Period = period,
            Entries = leaderboardData
        };
    });

    return result != null 
        ? Results.Ok(ApiResponse<LeaderboardResponse>.Ok(result))
        : Results.Problem("Failed to generate leaderboard");
});

// Get member's leaderboard positions
app.MapGet("/api/members/{identifier}/leaderboards", async (string identifier) =>
{
    var result = await ExecuteDbQuery(async db =>
    {
        var member = await FindMemberByIdentifier(db, identifier);
        if (member == null) return null;

        // This would calculate positions for various leaderboards
        var positions = new Dictionary<string, LeaderboardPosition>();

        // Example: Get kill leaderboard position
        var killPosition = await db.Database.SqlQuery<LeaderboardPosition>($@"
            WITH ranked_kills AS (
                SELECT 
                    membership_id,
                    SUM(value) as total_kills,
                    RANK() OVER (ORDER BY SUM(value) DESC) as rank,
                    COUNT(*) OVER() as total_players
                FROM member_activity_stats
                WHERE name = 'kills'
                GROUP BY membership_id
            )
            SELECT 
                'kills' as LeaderboardType,
                rank as Rank,
                total_players as TotalPlayers,
                total_kills as Value
            FROM ranked_kills
            WHERE membership_id = {member.MembershipId}
        ").FirstOrDefaultAsync();

        if (killPosition != null)
            positions["kills"] = killPosition;

        return new MemberLeaderboardStats
        {
            MembershipId = member.MembershipId.ToString(),
            DisplayName = member.DisplayNameGlobal ?? "",
            Positions = positions
        };
    });

    return result != null 
        ? Results.Ok(ApiResponse<MemberLeaderboardStats>.Ok(result))
        : Results.NotFound(ApiResponse<MemberLeaderboardStats>.Fail("Member not found"));
});

// ========== ACTIVITY ENDPOINTS ==========

// Get specific activity details
app.MapGet("/api/activities/{instanceId}", async (string instanceId) =>
{
    var result = await ExecuteDbQuery(async db =>
    {
        if (!long.TryParse(instanceId, out var instanceIdLong))
            return null;

        var instance = await db.Instances
            .FirstOrDefaultAsync(i => i.InstanceId == instanceIdLong);

        if (instance == null) return null;

        // Get instance members
        var instanceMembers = await db.InstanceMembers
            .Where(im => im.InstanceId == instanceIdLong)
            .Join(db.Members,
                im => im.MembershipId,
                m => m.MembershipId,
                (im, m) => new { InstanceMember = im, Member = m })
            .ToListAsync();

        return new
        {
            InstanceId = instance.InstanceId.ToString(),
            ActivityHash = instance.ActivityHash.ToString(),
            Completed = instance.Completed,
            IsPrivate = instance.IsPrivate,
            OccurredAt = DateTimeOffset.FromUnixTimeSeconds(instance.OccurredAt).UtcDateTime,
            Members = instanceMembers.Select(im => new
            {
                MembershipId = im.Member.MembershipId.ToString(),
                DisplayName = im.Member.DisplayNameGlobal ?? "",
                CharacterId = im.InstanceMember.CharacterId.ToString(),
                ClassName = im.InstanceMember.ClassName,
                LightLevel = im.InstanceMember.LightLevel,
                Completed = im.InstanceMember.Completed,
                ClanName = im.InstanceMember.ClanName,
                ClanTag = im.InstanceMember.ClanTag
            }).ToList()
        };
    });

    return result != null 
        ? Results.Ok(result)
        : Results.NotFound("Activity not found");
});

// ========== HEALTH/STATUS ENDPOINTS ==========

// Health check
app.MapGet("/api/health", async () =>
{
    var dbHealthy = false;
    try
    {
        await using var db = await RasputinDatabase.Connect();
        dbHealthy = await db.Database.CanConnectAsync();
    }
    catch { }

    return Results.Ok(new
    {
        Status = dbHealthy ? "Healthy" : "Unhealthy",
        Database = dbHealthy ? "Connected" : "Disconnected",
        Timestamp = DateTime.UtcNow
    });
});

// Get API info
app.MapGet("/api", () => Results.Ok(new
{
    Name = "Rasputin API",
    Version = "1.0",
    Description = "Destiny 2 data tracking and analytics API for Level Crush community",
    Documentation = "/swagger"
}));

// Helper methods
string GetClassName(string? classHash)
{
    return classHash switch
    {
        "671679327" => "Hunter",
        "2271682572" => "Warlock",
        "3655393761" => "Titan",
        _ => "Unknown"
    };
}

string GetModeName(int mode)
{
    return mode switch
    {
        0 => "None",
        2 => "Story",
        3 => "Strike",
        4 => "Raid",
        5 => "AllPvP",
        6 => "Patrol",
        7 => "AllPvE",
        9 => "Reserved9",
        10 => "Control",
        11 => "Reserved11",
        12 => "Clash",
        13 => "Reserved13",
        15 => "CrimsonDoubles",
        16 => "Nightfall",
        17 => "HeroicNightfall",
        18 => "AllStrikes",
        19 => "IronBanner",
        20 => "Reserved20",
        21 => "Reserved21",
        22 => "Reserved22",
        24 => "Reserved24",
        25 => "AllMayhem",
        26 => "Reserved26",
        27 => "Reserved27",
        28 => "Reserved28",
        29 => "Reserved29",
        30 => "Reserved30",
        31 => "Supremacy",
        32 => "PrivateMatchesAll",
        37 => "Survival",
        38 => "Countdown",
        39 => "TrialsOfTheNine",
        40 => "Social",
        41 => "TrialsCountdown",
        42 => "TrialsSurvival",
        43 => "IronBannerControl",
        44 => "IronBannerClash",
        45 => "IronBannerSupremacy",
        46 => "ScoredNightfall",
        47 => "ScoredHeroicNightfall",
        48 => "Rumble",
        49 => "AllDoubles",
        50 => "Doubles",
        51 => "PrivateMatchesClash",
        52 => "PrivateMatchesControl",
        53 => "PrivateMatchesSupremacy",
        54 => "PrivateMatchesCountdown",
        55 => "PrivateMatchesSurvival",
        56 => "PrivateMatchesMayhem",
        57 => "PrivateMatchesRumble",
        58 => "HeroicAdventure",
        59 => "Showdown",
        60 => "Lockdown",
        61 => "Scorched",
        62 => "ScorchedTeam",
        63 => "Gambit",
        64 => "AllPvECompetitive",
        65 => "Breakthrough",
        66 => "BlackArmoryRun",
        67 => "Salvage",
        68 => "IronBannerSalvage",
        69 => "PvPCompetitive",
        70 => "PvPQuickplay",
        71 => "ClashQuickplay",
        72 => "ClashCompetitive",
        73 => "ControlQuickplay",
        74 => "ControlCompetitive",
        75 => "GambitPrime",
        76 => "Reckoning",
        77 => "Menagerie",
        78 => "VexOffensive",
        79 => "NightmareHunt",
        80 => "Elimination",
        81 => "Momentum",
        82 => "Dungeon",
        83 => "Sundial",
        84 => "TrialsOfOsiris",
        85 => "Dares",
        86 => "Offensive",
        87 => "LostSector",
        88 => "Rift",
        89 => "ZoneControl",
        90 => "IronBannerRift",
        91 => "IronBannerZoneControl",
        _ => $"Unknown ({mode})"
    };
}

(DateTime? startDate, DateTime? endDate) GetPeriodDates(string period)
{
    var now = DateTime.UtcNow;
    return period.ToLower() switch
    {
        "daily" => (now.Date, now.Date.AddDays(1)),
        "weekly" => (now.AddDays(-7), now),
        "monthly" => (now.AddDays(-30), now),
        "season" => (now.AddDays(-90), now), // Approximate
        _ => (null, null)
    };
}

await app.RunAsync();