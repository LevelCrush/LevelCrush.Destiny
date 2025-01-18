using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class DestinyPostGameReportTeamEntry
{
    [JsonPropertyName("teamId")]
    public int TeamId { get; set; }
    
    [JsonPropertyName("standing")]
    public DestinyHistoricalStatsValue Standing { get; set; }
    
    [JsonPropertyName("score")]
    public DestinyHistoricalStatsValue Score { get; set; }
    
    [JsonPropertyName("teamName")]
    public string TeamName { get; set; }
}