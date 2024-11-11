using StackExchange.Redis;

namespace Rasputin_Redis;

public static class RasputinRedis
{
    private static readonly ConnectionMultiplexer _redis;

    static RasputinRedis()
    {
        _redis = ConnectionMultiplexer.Connect("127.0.0.1");
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