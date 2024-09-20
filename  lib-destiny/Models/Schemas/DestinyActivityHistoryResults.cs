using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

/// Activity history tied to a character
///
/// **Source** [Bungie Documentation](https://bungie-net.github.io/#Destiny2.GetActivityHistory)
public class DestinyActivityHistoryResults
{
    [JsonPropertyName("activities")]
    public DestinyHistoricalStatsPeriodGroup[] Activities { get; set; }
}