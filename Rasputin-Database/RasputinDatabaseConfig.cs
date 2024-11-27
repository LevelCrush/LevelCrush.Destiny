using System.Runtime.Serialization;
using Microsoft.Extensions.Configuration;

namespace Rasputin.Database;

[DataContract]
public class RasputinDatabaseConfig
{
    [DataMember] 
    public string Type { get; set; } = "local";
    
    [DataMember]
    public string Host { get; set; } = "localhost";
    
    [DataMember]
    public string Username { get; set; } = "";
    
    [DataMember]
    public string Password { get; set; } = "";

    [DataMember] 
    public uint Port { get; set; } = 3306;

    [DataMember] 
    public string Database { get; set; } = "defaultDb";

    [DataMember]
    public int PoolSize { get; set; } = 1;

    
    public static RasputinDatabaseConfig Load()
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

        var section = config.GetRequiredSection("RasputinDatabase");
        var appConfig = section.Get<RasputinDatabaseConfig>();
        if (appConfig == null)
        {
            throw new Exception("Rasputin Database configuration could not be loaded");
        }

        return appConfig;
    }
}