using System.Runtime.Serialization;
using Microsoft.Extensions.Configuration;

namespace Rasputin_Redis;

public class RasputinRedisConfig
{
    [DataMember] 
    public string Host { get; set; } = "127.0.0.1";
    
    public static RasputinRedisConfig Load()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", false, false)
            .AddJsonFile("appsettings.test.json", true, false)
            .AddJsonFile("appsettings.development.json", true, false)
            .AddJsonFile("appsettings.staging.json", true, false)
            .AddJsonFile("appsettings.production.json", true, false)
            .AddJsonFile("appsettings.local.json", true, false)
            .Build();

        var section = config.GetRequiredSection("RasputinRedis");
        var appConfig = section.Get<RasputinRedisConfig>();
        if (appConfig == null)
        {
            throw new Exception("Rasputin Database configuration could not be loaded");
        }

        return appConfig;
    }
}