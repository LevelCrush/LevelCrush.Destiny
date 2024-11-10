using Rasputin.MessageQueue.Models;

namespace Rasputin.MessageQueue.Consumer;

public static class ConsumerDBSync
{
    public static async Task<bool> Process(MessageDBSync message)
    {
        return false;
    }

}