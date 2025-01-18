using System.Runtime.Serialization;
using Microsoft.Extensions.Configuration;

namespace Destiny;

[DataContract]
public class DestinyConfig
{
    [DataMember]
    public string ApiKey { get; set; }

    [DataMember]
    public List<int> NetworkClans { get; set; }

    [DataMember] 
    public string? ClientId { get; set; }
    
    [DataMember]
    public string? ClientSecret { get; set; }
    


    public static DestinyConfig Load()
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

        var section = config.GetRequiredSection("Destiny");
        var destinyConfig = section.Get<DestinyConfig>();
        if (destinyConfig == null)
        {
            throw new Exception("Destiny configuration could not be loaded");
        }

        return destinyConfig;
    }
}