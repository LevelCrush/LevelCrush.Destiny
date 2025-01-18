using System.Text.Json.Serialization;

namespace Destiny.Models.Manifests;

public class DestinySeasonAct
{
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("startTime")] 
    public DateTime StartTime { get; set; } = DateTime.UnixEpoch;

    [JsonPropertyName("rankCount")] 
    public uint RankCount { get; set; } = 0;
}