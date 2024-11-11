using System.Collections.Concurrent;
using System.Text.Json.Serialization;
using Rasputin.MessageQueue.Enums;

namespace Rasputin.MessageQueue.Models;

public class MessageDbSync
{
    [JsonPropertyName("task")] 
    public MessageDbSyncTask Task { get; set; } = MessageDbSyncTask.None;
    
    [JsonPropertyName("data")]
    public string Data { get; set; } = string.Empty;
    
    [JsonPropertyName("headers")]
    public Dictionary<string,string> Headers { get; set; } = new Dictionary<string, string>();
}