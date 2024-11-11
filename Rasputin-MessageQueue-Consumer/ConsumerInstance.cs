using System.Collections.Concurrent;
using System.Text.Json;
using Destiny.Api;
using Destiny.Models.Enums;
using Rasputin.Database;
using Rasputin.Database.Models;
using Rasputin.MessageQueue.Enums;
using Rasputin.MessageQueue.Models;
using Rasputin.MessageQueue.Queues;

namespace Rasputin.MessageQueue.Consumer;

public static class ConsumerInstance
{
    public static async Task<bool>  Process(MessageInstance instance)
    {
        await ProcessInstance(instance.Entities);
        return false;
    }

    private static async Task<bool> ProcessInstance(string[] entities)
    {
        long[] instanceIds = new long[entities.Length];
        var i = 0;
        foreach (var entity in entities)
        {
            long.TryParse(entity, out instanceIds[i++]);
        }

        foreach (var instanceId in instanceIds)
        {
            var carnageReport = await DestinyInstance.CarnageReport(instanceId);
            QueueDBSync.Publish(new MessageDbSync()
            {
                Task = MessageDbSyncTask.Instance,
                Data = JsonSerializer.Serialize(carnageReport)
            });
        }
        
        return true;
    }
}