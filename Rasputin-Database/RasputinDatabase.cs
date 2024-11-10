using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySqlConnector;
using Rasputin.Database.Models;

namespace Rasputin.Database;

public class RasputinDatabase
{
    private static PooledDbContextFactory<DBDestinyContext> _factory;
    private static RasputinDatabaseConfig _config;
    static RasputinDatabase()
    {
  
        _config = RasputinDatabaseConfig.Load();
        
        var builder = new MySqlConnectionStringBuilder();
        builder.Server = _config.Host;
        builder.Database = _config.Database;
        builder.UserID = _config.Username;
        builder.Password = _config.Password;
        builder.Port = _config.Port;
        var connectionString = builder.ToString();
        
        //options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        var options = new DbContextOptionsBuilder<DBDestinyContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
        
        _factory = new PooledDbContextFactory<DBDestinyContext>(options, _config.PoolSize);
    }

    public static async Task<DBDestinyContext> Connect()
    {
        return await _factory.CreateDbContextAsync();
    }
}