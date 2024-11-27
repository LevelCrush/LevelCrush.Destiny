using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class UserMembershipData
{
    [JsonPropertyName("destinyMemberships")]
    public ConcurrentQueue<UserInfoCard> Memberships { get; set; }
}