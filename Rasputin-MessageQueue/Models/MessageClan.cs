using System.Text.Json.Serialization;
using Rasputin.MessageQueue.Enums;

namespace Rasputin.MessageQueue.Models;

public class MessageClan
{
    [JsonPropertyName("task")] 
    public MessageClanTask Task { get; set; } = MessageClanTask.Info;

    [JsonPropertyName("entities")] 
    public string[] Entities { get; set; } = [];
}