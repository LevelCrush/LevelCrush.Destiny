using System.Runtime.Serialization;
using System.Runtime.Serialization.DataContracts;
using Destiny.Models.Enums;

namespace Destiny.Models.Responses;

[DataContract]
public class BungieResponse<T> where T : class
{
    [DataMember(Name = "Response")]
    public T? Response { get; set; }

    [DataMember(Name = "ErrorCode")]
    public PlatformErrorCode ErrorCode { get; set; }

    [DataMember(Name = "ThrottleSeconds")]
    public ulong ThrottleSeconds { get; set; }

    [DataMember(Name = "Message")]
    public string Message { get; set; }

    [DataMember(Name = "MessageData")]
    public Dictionary<string, string> MessageData { get; set; }


    public bool IsThrottled()
    {
        return ErrorCode != PlatformErrorCode.Success && ThrottleSeconds > 0;
    }
}