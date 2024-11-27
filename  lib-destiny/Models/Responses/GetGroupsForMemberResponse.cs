using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class GetGroupsForMemberResponse
{
    [JsonPropertyName("areAllMembershipsInactive")]
    public ConcurrentDictionary<string, bool> AreAllMembershipsInactive { get; set; } = new();
    
    [JsonPropertyName("results")]
    public GroupMembership[] Results { get; set; }
}