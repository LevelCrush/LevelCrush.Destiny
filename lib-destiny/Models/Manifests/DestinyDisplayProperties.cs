using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Destiny.Models.Manifests;

public class DestinyDisplayProperties
{
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("name")] 
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("icon")] 
    public string Icon { get; set; } = string.Empty;

    [JsonPropertyName("hasIcon")]
    public bool HasIcon { get; set; } = false;

}