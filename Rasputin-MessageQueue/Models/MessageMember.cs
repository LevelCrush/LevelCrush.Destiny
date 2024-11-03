using System.Text.Json.Serialization;
using Rasputin.MessageQueue.Enums;

namespace Rasputin.MessageQueue.Models;

public class MessageMember
{
    [JsonPropertyName("task")] 
    public MessageMemberTask Task { get; set; } = MessageMemberTask.Info;

    [JsonPropertyName("entities")] 
    public string[] Entities { get; set; } = [];
}