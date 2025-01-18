using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class GroupV2
{
    [JsonPropertyName("groupId")]
    public long GroupId { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
   
    [JsonPropertyName("about")]
    public string About { get; set; }
    
    [JsonPropertyName("memberCount")]
    public int MemberCount { get; set; }
    
    [JsonPropertyName("motto")]
    public string Motto { get; set; }
    
    [JsonPropertyName("theme")]
    public string Theme { get; set; }
    
    
    [JsonPropertyName("bannerPath")]
    public string BannerPath { get; set; }
    
    [JsonPropertyName("avatarPath")]
    public string AvatarPath { get; set; }

    [JsonPropertyName("remoteGroupId")] 
    public long RemoteGroupId { get; set; } = 0;
    
    [JsonPropertyName("clanInfo")]
    public GroupV2ClanInfoAndInvestment ClanInfo { get; set; }
}