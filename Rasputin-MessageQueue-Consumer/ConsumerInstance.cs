using Rasputin.MessageQueue.Models;

namespace Rasputin.MessageQueue.Consumer;

public static class ConsumerInstance
{
    public static async Task<bool>  Process(MessageInstance instance)
    {
        return false;
    }
}