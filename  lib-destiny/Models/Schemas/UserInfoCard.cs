using System.Runtime.Serialization;
using Destiny.Models.Enums;

namespace Destiny.Models.Schemas;

[DataContract]
public class UserInfoCard
{
    [DataMember(Name = "applicableMembershipTypes")]
    public BungieMembershipType[] ApplicableMembershipTypes { get; set; }

    [DataMember(Name = "crossSaveOverride")]
    public BungieMembershipType CrossSaveOverride { get; set; }

    [DataMember(Name = "membershipId")]
    public string MembershipId { get; set; }

    [DataMember(Name = "membershipType")]
    public BungieMembershipType MembershipType { get; set; }

    [DataMember(Name = "displayName")]
    public string DisplayName { get; set; }

    [DataMember(Name = "supplementalDisplayName")]
    public string SupplementalDisplayName { get; set; }

    [DataMember(Name = "LastSeenDisplayName")]
    public string LastSeenDisplayName { get; set; }

    [DataMember(Name = "LastSeenDisplayNameType")]
    public int LastSeenPlatform { get; set; }

    [DataMember(Name = "bungieGlobalDisplayName")]
    public string GlobalDisplayName { get; set; }

    [DataMember(Name = "bungieGlobalDisplayNameCode")]
    public int GlobalDisplayNameCode { get; set; }
}