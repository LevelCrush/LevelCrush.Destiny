using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class DestinyHistoricalStatsWithMerged
{
    [JsonPropertyName("merged")]
    public DestinyHistoricalStatsByPeriod Merged { get; set; }
}