using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class GroupMember
{
    [JsonPropertyName("memberType")]
    public uint MemberType { get; set; }
    
    [JsonPropertyName("isOnline")]
    public bool IsOnline { get; set; }
    
    [JsonPropertyName("groupId")]
    public long GroupId { get; set; }
    
    [JsonPropertyName("destinyUserInfo")]
    public UserInfoCard UserInfo { get; set; }

    [JsonPropertyName("bungieNetUserInfo")]
    public UserInfoCard BungieInfo { get; set; } = new();

    [JsonPropertyName("joinDate")] 
    public DateTime JoinDate { get; set; } = DateTime.UnixEpoch;

}