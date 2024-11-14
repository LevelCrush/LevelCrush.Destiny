using System.Text.Json;
using Destiny.Api;
using Destiny.Models.Manifests;
using Microsoft.Extensions.Logging;
using Rasputin.Database.Models;
using Rasputin.MessageQueue.Enums;
using Rasputin.MessageQueue.Models;
using Rasputin.MessageQueue.Queues;

namespace Rasputin.MessageQueue.Consumer;

public class ConsumerActions
{
    public static async Task<bool> Process(MessageAction action)
    {
        switch (action.Action.ToLower().Trim())
        {
            case "manifest:sync":
                await TaskManifestSync();
                break;
            default:
                LoggerGlobal.Write($"Unknown action: {action.Action}");
                break;
        }
        return true;
    }

    public static async Task<bool> TaskManifestSync()
    {

        LoggerGlobal.Write($"Fetching class definitions");
        var classDefinitions = await DestinyManifest.Get<DestinyClassDefinition>();
        
        LoggerGlobal.Write($"Publishing class definitions");
        QueueDBSync.Publish(new MessageDbSync()
        {
            Task = MessageDbSyncTask.ManifestClassDefinitions,
            Data = JsonSerializer.Serialize(classDefinitions)
        });
        
        
        LoggerGlobal.Write($"Fetching activity definitions");
        var activityDefinitions = await DestinyManifest.Get<DestinyActivityDefinition>();
        
        LoggerGlobal.Write($"Publishing activity definitions");
        QueueDBSync.Publish(new MessageDbSync()
        {
            Task = MessageDbSyncTask.ManifestActivityDefinitions,
            Data = JsonSerializer.Serialize(activityDefinitions)
        });

        LoggerGlobal.Write($"Fetching activity type definitions");
        var activityTypeDefintions = await DestinyManifest.Get<DestinyActivityTypeDefinition>();
        QueueDBSync.Publish(new MessageDbSync()
        {
            Task = MessageDbSyncTask.ManifestActivityTypeDefinitions,
            Data = JsonSerializer.Serialize(activityTypeDefintions)
        });

        LoggerGlobal.Write($"Fetching season definitions");
        var seasonDefinitions = await DestinyManifest.Get<DestinySeasonDefinition>();
        
        LoggerGlobal.Write($"Publishing season definitions");
        QueueDBSync.Publish(new MessageDbSync()
        {
            Task = MessageDbSyncTask.ManifestSeasonDefinitions,
            Data = JsonSerializer.Serialize(seasonDefinitions)
        });
        
        LoggerGlobal.Write($"Fetching triumph record definitions");
        var triumphDefinitions = await DestinyManifest.Get<DestinyRecordDefinition>();
        
        LoggerGlobal.Write($"Publishing triumph record definitions");
        QueueDBSync.Publish(new MessageDbSync()
        {
            Task = MessageDbSyncTask.ManifestTriumphDefinitions,
            Data= JsonSerializer.Serialize(triumphDefinitions)
        });
        
        
        LoggerGlobal.Write("Done publishing known manifest data");
        
        return true;
    }
}