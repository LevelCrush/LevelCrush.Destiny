using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class DestinyProfileRecordsComponent
{
    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("activeScore")]
    public int ActiveScore { get; set; }

    [JsonPropertyName("legacyScore")]
    public int LegacyScore { get; set; }

    [JsonPropertyName("lifetimeScore")]
    public int LifetimeScore { get; set; }


    [JsonPropertyName("records")]
    public ConcurrentDictionary<string, DestinyRecordComponent> Records { get; set; }
}