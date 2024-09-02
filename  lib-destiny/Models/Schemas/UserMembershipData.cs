using System.Collections.Concurrent;
using System.Runtime.Serialization;

namespace Destiny.Models.Schemas;

[DataContract]
public class UserMembershipData
{
    [DataMember(Name = "destinyMemberships")]
    public ConcurrentQueue<UserInfoCard> Memberships { get; set; }
}