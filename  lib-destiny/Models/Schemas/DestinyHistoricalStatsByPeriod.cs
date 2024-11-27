using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class DestinyHistoricalStatsByPeriod
{
    [JsonPropertyName("allTime")]
    public ConcurrentDictionary<string, DestinyHistoricalStatsValue> AllTime { get; set; } = new();
}