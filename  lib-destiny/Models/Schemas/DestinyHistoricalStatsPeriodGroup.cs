using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class DestinyHistoricalStatsPeriodGroup
{
    [JsonPropertyName("period")]
    public string Period { get; set; }
    
    [JsonPropertyName("values")]
    public ConcurrentDictionary<string, DestinyHistoricalStatsValue> Values { get; set; }
    
    [JsonPropertyName("activityDetails")]
    public DestinyHistoricalStatsActivity Details { get; set; }
}