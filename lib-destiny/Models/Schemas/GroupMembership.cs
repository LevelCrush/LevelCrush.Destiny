using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class GroupMembership
{
    [JsonPropertyName("member")] 
    public GroupMember Member { get; set; } = new();
    
    [JsonPropertyName("group")]
    public GroupV2 Group { get; set; } = new();
    
}