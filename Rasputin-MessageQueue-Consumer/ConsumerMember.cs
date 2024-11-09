using Rasputin.MessageQueue.Enums;
using Rasputin.MessageQueue.Models;

namespace Rasputin.MessageQueue.Consumer;

public static class ConsumerMember
{
    public static async Task<bool> Process(MessageMember message)
    {

        switch (message.Task)
        {
            case MessageMemberTask.Info:
                await TaskInfo(message.Entities);
                break;
        }
        
        
        
        return false;
    }

    private static async Task<Dictionary<string, bool>> TaskInfo(string[] entities)
    {
        var result = new Dictionary<string, bool>();

        return result;
    }
}