using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Destiny.Models.Schemas;

namespace Destiny.Models.Responses;

/// The response for GetDestinyProfile, with components for character and item-level data.
/// 
/// **Note**: This does not contain all available properties of a full Destiny Profile response. Only what we need
/// 
/// **Source**: [Bungie Official Documentation](https://bungie-net.github.io/#/components/schemas/Destiny.Responses.DestinyProfileResponse)
public class DestinyProfileResponse
{
    /// Records the timestamp of when most components were last generated from the world server source.
    /// Unless the component type is specified in the documentation for secondaryComponentsMintedTimestamp,
    /// this value is sufficient to do data freshness.
    [JsonPropertyName("responseMintedTimestamp")]
    public DateTime TimestampResponse { get; set; } = DateTime.UnixEpoch;

    /// Some secondary components are not tracked in the primary response timestamp and have their timestamp tracked here. If your component is any of the following, this field is where you will find your timestamp value:
    /// 
    /// PresentationNodes, Records, Collectibles, Metrics, StringVariables, Craftables, Transitory
    /// 
    /// All other component types may use the primary timestamp property.
    [JsonPropertyName("secondaryComponentsMintedTimestamp")]
    public DateTime TimestampSecondaryComponents { get; set; } = DateTime.UnixEpoch;

    [JsonPropertyName("profile")]
    public DestinyComponent<DestinyProfileComponent>? Profile { get; set; }

    [JsonPropertyName("characters")]
    public DestinyComponent<Dictionary<string, DestinyCharacterComponent>>? Characters { get; set; }

    [JsonPropertyName("profileRecords")]
    public DestinyComponent<DestinyProfileRecordsComponent>? ProfileRecords { get; set; }
}