using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class DestinyHistoricalStatsValue
{
    
    [JsonPropertyName("statId")]
    public string StatId { get; set; }
    
    [JsonPropertyName("activityId")]
    public string ActivityId { get; set; }

    [JsonPropertyName("basic")]
    public DestinyHistoricalStatsValuePair Basic { get; set; }
    
    [JsonPropertyName("pga")]
    public DestinyHistoricalStatsValuePair? Pga { get; set; }
    
    [JsonPropertyName("weighted")]
    public DestinyHistoricalStatsValuePair? Weighted { get; set; }
}