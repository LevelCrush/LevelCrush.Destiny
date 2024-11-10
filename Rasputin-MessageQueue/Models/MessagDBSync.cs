using System.Text.Json.Serialization;
using Rasputin.MessageQueue.Enums;

namespace Rasputin.MessageQueue.Models;

public class MessageDBSync
{
    [JsonPropertyName("task")]
    public MessageDBSyncTask Task { get; set; }
    
    [JsonPropertyName("data")]
    public string Data { get; set; }
}