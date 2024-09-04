using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class DestinyRecordComponent
{
    [JsonPropertyName("state")]
    public int State { get; set; }

    [JsonPropertyName("completedCount")]
    public int CompletedCount { get; set; }
}