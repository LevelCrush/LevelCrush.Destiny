namespace Rasputin_Server.Model;

public class ClanSummaryResponse
{
    public string GroupId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string? Motto { get; set; }
    public string? About { get; set; }
    public string? CallSign { get; set; }
    public bool IsNetwork { get; set; }
    public int MemberCount { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public class ClanRosterResponse
{
    public string GroupId { get; set; } = string.Empty;
    public string ClanName { get; set; } = string.Empty;
    public int MemberCount { get; set; }
    public List<ClanMemberDetails> Members { get; set; } = new();
}

public class ClanMemberDetails
{
    public string MembershipId { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string DisplayNameGlobal { get; set; } = string.Empty;
    public int Platform { get; set; }
    public int GroupRole { get; set; }
    public string RoleName => GetRoleName(GroupRole);
    public DateTime? JoinedAt { get; set; }
    public DateTime? LastPlayedAt { get; set; }
    public int? GuardianRankCurrent { get; set; }

    private static string GetRoleName(int role)
    {
        return role switch
        {
            0 => "None",
            1 => "Beginner",
            2 => "Member",
            3 => "Admin",
            4 => "Acting Founder",
            5 => "Founder",
            _ => "Unknown"
        };
    }
}

public class ClanActivityStats
{
    public string GroupId { get; set; } = string.Empty;
    public string ClanName { get; set; } = string.Empty;
    public int TotalActivities { get; set; }
    public int ActiveMembers { get; set; }
    public Dictionary<string, int> ActivitiesByMode { get; set; } = new();
    public List<TopActivity> MostPlayedActivities { get; set; } = new();
    public DateTime? EarliestActivity { get; set; }
    public DateTime? LatestActivity { get; set; }
}

public class TopActivity
{
    public string ActivityHash { get; set; } = string.Empty;
    public string? ActivityName { get; set; }
    public int PlayCount { get; set; }
    public int UniqueMembers { get; set; }
    public double CompletionRate { get; set; }
}