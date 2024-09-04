using System.Text.Json.Serialization;

namespace Destiny.Models.Requests;

public class UserExactSearchRequest
{
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("displayNameCode")]
    public short Code { get; set; }
}