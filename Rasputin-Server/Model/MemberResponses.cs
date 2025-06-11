namespace Rasputin_Server.Model;

public class MemberSummaryResponse
{
    public string MembershipId { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string DisplayNameGlobal { get; set; } = string.Empty;
    public int Platform { get; set; }
    public int? GuardianRankCurrent { get; set; }
    public int? GuardianRankLifetime { get; set; }
    public DateTime? LastPlayedAt { get; set; }
    public List<CharacterSummary> Characters { get; set; } = new();
    public List<ClanMembershipSummary> Clans { get; set; } = new();
}

public class CharacterSummary
{
    public string CharacterId { get; set; } = string.Empty;
    public string ClassHash { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public int LightLevel { get; set; }
    public long MinutesPlayedSession { get; set; }
    public long MinutesPlayedLifetime { get; set; }
    public DateTime? LastPlayedAt { get; set; }
    public string? EmblemUrl { get; set; }
}

public class ClanMembershipSummary
{
    public string GroupId { get; set; } = string.Empty;
    public string ClanName { get; set; } = string.Empty;
    public string? ClanTag { get; set; }
    public int GroupRole { get; set; }
    public DateTime? JoinedAt { get; set; }
}

public class MemberActivityStats
{
    public string MembershipId { get; set; } = string.Empty;
    public int TotalActivities { get; set; }
    public Dictionary<string, int> ActivitiesByMode { get; set; } = new();
    public Dictionary<string, double> AverageStats { get; set; } = new();
    public DateTime? FirstActivity { get; set; }
    public DateTime? LastActivity { get; set; }
}

public class MemberRecentActivities
{
    public string MembershipId { get; set; } = string.Empty;
    public List<ActivitySummary> Activities { get; set; } = new();
}

public class ActivitySummary
{
    public string InstanceId { get; set; } = string.Empty;
    public string ActivityHash { get; set; } = string.Empty;
    public string? ActivityName { get; set; }
    public int Mode { get; set; }
    public bool Completed { get; set; }
    public DateTime OccurredAt { get; set; }
    public Dictionary<string, double> Stats { get; set; } = new();
}