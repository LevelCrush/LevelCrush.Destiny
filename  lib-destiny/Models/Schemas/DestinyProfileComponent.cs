using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

/// Contains more relevant information about a membership profile
/// 
/// **Source** [Bungie Documentation](https://bungie-net.github.io/#/components/schemas/Destiny.Entities.Profiles.DestinyProfileComponent)
public class DestinyProfileComponent
{
    [JsonPropertyName("dateLastPlayed")] 
    public DateTime DateLastPlayed { get; set; } = DateTime.UnixEpoch;

    [JsonPropertyName("userInfo")]
    public UserInfoCard UserInfo { get; set; }

    [JsonPropertyName("characterIds")]
    public long[] Characters { get; set; }

    [DataMember(Name = "seasonHashes", IsRequired = false)]
    public uint[] SeasonHashes { get; set; }

    [JsonPropertyName("currentGuardianRank")]
    public int GuardianRankCurrent { get; set; }

    [JsonPropertyName("lifetimeHighestGuardianRank")]
    public int GuardianRankLifetime { get; set; }
}