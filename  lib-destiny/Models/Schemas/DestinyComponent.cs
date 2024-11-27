using System.Text.Json.Serialization;

namespace Destiny.Models.Schemas;

/// Contains component data as well as profile privacy and if it is disabled (if available)
/// 
/// **Source**: [Bungie Documentation](https://bungie-net.github.io/#/components/schemas/SingleComponentResponseOfDestinyProfileComponent)
public class DestinyComponent<T> where T : class
{
    [JsonPropertyName("disabled")]
    public bool? Disabled { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("privacy")]
    public int Privacy { get; set; }
}