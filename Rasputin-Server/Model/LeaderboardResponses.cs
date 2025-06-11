namespace Rasputin_Server.Model;

public class LeaderboardEntry
{
    public int Rank { get; set; }
    public string MembershipId { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? ClanName { get; set; }
    public string? ClanTag { get; set; }
    public double Value { get; set; }
    public string? ValueDisplay { get; set; }
}

public class LeaderboardResponse
{
    public string LeaderboardType { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public List<LeaderboardEntry> Entries { get; set; } = new();
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}

public class MemberLeaderboardStats
{
    public string MembershipId { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public Dictionary<string, LeaderboardPosition> Positions { get; set; } = new();
}

public class LeaderboardPosition
{
    public string LeaderboardType { get; set; } = string.Empty;
    public int Rank { get; set; }
    public int TotalPlayers { get; set; }
    public double Percentile => (1.0 - (Rank / (double)TotalPlayers)) * 100;
    public double Value { get; set; }
}