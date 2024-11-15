using System.Collections.Concurrent;
using System.Text.Json;
using Destiny.Api;
using Destiny.Models.Enums;
using Microsoft.Extensions.Logging;
using Rasputin_Redis;
using Rasputin.Database;
using Rasputin.Database.Models;
using Rasputin.MessageQueue.Enums;
using Rasputin.MessageQueue.Models;
using Rasputin.MessageQueue.Queues;
using StackExchange.Redis;

namespace Rasputin.MessageQueue.Consumer;

public static class ConsumerInstance
{
    private static IDatabase _redis;

    static ConsumerInstance()
    {
        _redis = RasputinRedis.Connect();
    }
    
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
            var cacheKey = $"rasputin-instance-{instanceId}";
            var alreadyScraped = await _redis.KeyExistsAsync(cacheKey);
            if (alreadyScraped)
            {
                LoggerGlobal.Write($"Instance already retrieved. Skipping {instanceId}");
                var ttl = await _redis.KeyTimeToLiveAsync(cacheKey);
                if (ttl != null)
                {
                    LoggerGlobal.Write($"TTL on {instanceId} is {ttl.Value.TotalSeconds} seconds");
                }
                    
                continue;
            }

            try
            {
                var carnageReport = await DestinyInstance.CarnageReport(instanceId);
                if (carnageReport != null)
                {
                    LoggerGlobal.Write($"Publishing  data for {instanceId} to the database queue");
                    QueueDBSync.Publish(new MessageDbSync()
                    {
                        Task = MessageDbSyncTask.Instance,
                        Data = JsonSerializer.Serialize(carnageReport)
                    });
                    LoggerGlobal.Write($"Done Publishing data for {instanceId} to the database queue");
                    LoggerGlobal.Write($"Setting key in redis for {instanceId}");

                    await _redis.StringSetAsync(cacheKey, "1", TimeSpan.FromDays(1), true);
                    LoggerGlobal.Write($"Done Setting key in redis for {instanceId}");
                }
                else
                {
                    LoggerGlobal.Write($"Failed to deserialize instance {instanceId} as expected");
                }
            }
            catch (Exception e)
            {
                LoggerGlobal.Write($"While processing {instanceId} an error occurred\r\n{e.Message}", LogLevel.Error);
            }
        }

        
        return true;
    }
}