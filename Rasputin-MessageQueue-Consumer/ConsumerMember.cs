using Rasputin.MessageQueue.Models;

namespace Rasputin.MessageQueue.Consumer;

public static class ConsumerMember
{
    public static async Task<bool> Process(MessageMember member)
    {
        return false;
    }
}