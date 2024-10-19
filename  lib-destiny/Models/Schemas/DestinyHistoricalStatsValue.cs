using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class DestinyHistoricalStatsValue
{
    
    [JsonPropertyName("statId")]
    public string StatId { get; set; }

    [JsonPropertyName("activityId")] 
    public long ActivityId { get; set; } = 0;

    [JsonPropertyName("basic")]
    public DestinyHistoricalStatsValuePair Basic { get; set; }
    
    [JsonPropertyName("pga")]
    public DestinyHistoricalStatsValuePair? Pga { get; set; }
    
    [JsonPropertyName("weighted")]
    public DestinyHistoricalStatsValuePair? Weighted { get; set; }
}