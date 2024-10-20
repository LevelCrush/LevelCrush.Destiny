using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class DestinySearchResultOfGroupMember
{
    [JsonPropertyName("results")] 
    public GroupMember[] Results { get; set; } = [];
    
    [JsonPropertyName("totalResults")]
    public int TotalResults { get; set; }
    
    [JsonPropertyName("hasMore")]
    public bool HasMore { get; set; }

    [JsonPropertyName("query")] 
    public PagedQuery Query { get; set; } = new();

    [JsonPropertyName("replacementContinuationToken")]
    public string ReplacementToken { get; set; } = string.Empty;

}