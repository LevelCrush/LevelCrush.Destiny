using StackExchange.Redis;

namespace Rasputin_Redis;

public static class RasputinRedis
{
    private static readonly ConnectionMultiplexer _redis;

    static RasputinRedis()
    {
        var redisConfig = RasputinRedisConfig.Load();
        var config = ConfigurationOptions.Parse($"{redisConfig.Host}:{redisConfig.Port}");
        if (redisConfig.Password != "")
        {
            config.Password = redisConfig.Password;
        }

        if (redisConfig.Username != "")
        {
            config.User = redisConfig.Username;
        }

        if (redisConfig.Database >= 0)
        {
            config.DefaultDatabase = redisConfig.Database;
        }
        else
        {
            config.DefaultDatabase = 0;
        }
        
        _redis = ConnectionMultiplexer.Connect(config);
    }

    public static IDatabase Connect()
    {
        return _redis.GetDatabase();
    }

    public static async Task Close()
    {
        await _redis.CloseAsync(true);
    }
}