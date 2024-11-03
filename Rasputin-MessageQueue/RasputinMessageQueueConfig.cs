using System.Runtime.Serialization;
using Microsoft.Extensions.Configuration;

namespace Rasputin.MessageQueue;

[DataContract]
public class RasputinMessageQueueConfig
{
    [DataMember]
    public string Host { get; set; } = "localhost";
    
    [DataMember]
    public string Username { get; set; } = "";
    
    [DataMember]
    public string Password { get; set; } = "";

    [DataMember] 
    public uint Port { get; set; } = 5672;
    
    [DataMember]
    public string VirtualHost { get; set; } = "/";
    
    public static RasputinMessageQueueConfig Load()
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

        var section = config.GetRequiredSection("RasputinMessageQueue");
        var appConfig = section.Get<RasputinMessageQueueConfig>();
        if (appConfig == null)
        {
            throw new Exception("Rasputin Database configuration could not be loaded");
        }

        return appConfig;
    }
}