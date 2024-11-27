using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace Destiny.Models.Responses;

public class DestinyManifestResponse
{
    [JsonPropertyName("version")]
    public required string Version { get; set; }

    [JsonPropertyName("jsonWorldComponentContentPaths")]
    public required ConcurrentDictionary<string, ConcurrentDictionary<string, string>> JsonWorldComponentContentPath
    {
        get;
        set;
    }

    [JsonPropertyName("locale")] 
    public string Locale { get; set; } = "en";
}