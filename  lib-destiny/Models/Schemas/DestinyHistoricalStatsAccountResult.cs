using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class DestinyHistoricalStatsAccountResult
{
    [JsonPropertyName("mergedAllCharacters")]
    public DestinyHistoricalStatsWithMerged AllCharacters { get; set; }
}