using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class DestinyPostGameCarnageReportEntry
{
    [JsonPropertyName("standing")]
    public int Standing { get; set; }

    [JsonPropertyName("characterId")] 
    public long CharacterId { get; set; } = 0;
    
    [JsonPropertyName("score")]
    public DestinyHistoricalStatsValue Score { get; set; }
    
    [JsonPropertyName("values")]
    public ConcurrentDictionary<string, DestinyHistoricalStatsValue> Values { get; set; } = new();
    
    [JsonPropertyName("teamName")]
    public string TeamName { get; set; } = string.Empty;
    
}