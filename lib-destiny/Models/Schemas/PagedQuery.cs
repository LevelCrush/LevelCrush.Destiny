using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

public class PagedQuery
{
    [JsonPropertyName("itemsPerPage")]
    public int ItemsPerPage { get; set; }
    
    [JsonPropertyName("currentPage")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("requestContinuationToken")]
    public string RequestToken { get; set; } = string.Empty;
}