using System.Text.Json.Serialization;
using Destiny.Models.Enums;

namespace Destiny.Models.Responses;

public class BungieResponse<T> where T : class
{
    [JsonPropertyName("Response")]
    public T? Response { get; set; }

    [JsonPropertyName("ErrorCode")]
    public required  PlatformErrorCode ErrorCode { get; set; }

    [JsonPropertyName("ThrottleSeconds")]
    public required ulong ThrottleSeconds { get; set; }

    [JsonPropertyName("Message")]
    public required string Message { get; set; }

    [JsonPropertyName("MessageData")]
    public required Dictionary<string, string> MessageData { get; set; }


    public bool IsThrottled()
    {
        return ErrorCode != PlatformErrorCode.Success && ThrottleSeconds > 0;
    }
}