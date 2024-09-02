using System.Runtime.Serialization;

namespace Destiny.Models.Schemas;

/// Contains more relevant information about a membership profile
/// 
/// **Source** [Bungie Documentation](https://bungie-net.github.io/#/components/schemas/Destiny.Entities.Profiles.DestinyProfileComponent)
[DataContract]
public class DestinyProfileComponent
{
    [DataMember(Name = "dateLastPlayed")]
    public string DateLastPlayed { get; set; }

    [DataMember(Name = "userInfo")]
    public UserInfoCard UserInfoCard { get; set; }

    [DataMember(Name = "characterIds")]
    public string[] Characters { get; set; }

    [DataMember(Name = "currentGuardianRank")]
    public ushort GuardianRankCurrent { get; set; }

    [DataMember(Name = "lifetimeHighestGuardianRank")]
    public ushort GuardianRankLifetime { get; set; }
}