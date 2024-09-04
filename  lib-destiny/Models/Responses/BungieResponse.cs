using System.Text.Json.Serialization;
using Destiny.Models.Enums;

namespace Destiny.Models.Responses;

public class BungieResponse<T> where T : class
{
    [JsonPropertyName("Response")]
    public T? Response { get; set; }

    [JsonPropertyName("ErrorCode")]
    public PlatformErrorCode ErrorCode { get; set; }

    [JsonPropertyName("ThrottleSeconds")]
    public ulong ThrottleSeconds { get; set; }

    [JsonPropertyName("Message")]
    public string Message { get; set; }

    [JsonPropertyName("MessageData")]
    public Dictionary<string, string> MessageData { get; set; }


    public bool IsThrottled()
    {
        return ErrorCode != PlatformErrorCode.Success && ThrottleSeconds > 0;
    }
}