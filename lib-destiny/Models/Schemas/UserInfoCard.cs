using System.Text.Json.Serialization;
using Destiny.Models.Enums;

namespace Destiny.Models.Schemas;

public class UserInfoCard
{
    [JsonPropertyName("applicableMembershipTypes")]
    public BungieMembershipType[] ApplicableMembershipTypes { get; set; }

    [JsonPropertyName("crossSaveOverride")]
    public BungieMembershipType CrossSaveOverride { get; set; }

    [JsonPropertyName("membershipId")]
    public long MembershipId { get; set; }

    [JsonPropertyName("membershipType")]
    public BungieMembershipType MembershipType { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    /*
    [JsonPropertyName("supplementalDisplayName")]
    public string SupplementalDisplayName { get; set; }

    [JsonPropertyName("LastSeenDisplayName")]
    public string LastSeenDisplayName { get; set; }

    [JsonPropertyName("LastSeenDisplayNameType")]
    public int LastSeenPlatform { get; set; }
    */

    [JsonPropertyName("bungieGlobalDisplayName")]
    public string GlobalDisplayName { get; set; }

    [JsonPropertyName("bungieGlobalDisplayNameCode")]
    public int GlobalDisplayNameCode { get; set; }
}