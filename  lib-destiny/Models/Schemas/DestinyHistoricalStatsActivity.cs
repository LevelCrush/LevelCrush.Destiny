using System.Text.Json.Serialization;
using Destiny.Models.Enums;

namespace Destiny.Models.Schemas;

/// Information about the activity
///
/// **Source** [Bungie Documentation](https://bungie-net.github.io/#/components/schemas/Destiny.HistoricalStats.DestinyHistoricalStatsActivity)
public class DestinyHistoricalStatsActivity
{
    [JsonPropertyName("referenceId")]
    public string ReferenceId { get; set; }
    
    [JsonPropertyName("directorActivityHash")]
    public uint DirectorActivityHash { get; set; }
    
    [JsonPropertyName("instanceId")]
    public string InstanceId { get; set; }
    
    [JsonPropertyName("isPrivate")]
    public bool IsPrivate { get; set; }
    
    [JsonPropertyName("mode")]
    public DestinyActivityModeType Mode { get; set; }
    
    [JsonPropertyName("modes")]
    public DestinyActivityModeType[] Modes { get; set; }
    
    [JsonPropertyName("membershipType")]
    public BungieMembershipType MembershipType { get; set; }
}