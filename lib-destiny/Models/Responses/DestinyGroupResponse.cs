using System.Text.Json.Serialization;
using Destiny.Models.Schemas;

namespace Destiny.Models.Responses;

public class DestinyGroupResponse
{
    [JsonPropertyName("detail")]
    public GroupV2 Detail { get; set; }
}