using RabbitMQ.Client;

namespace Rasputin.MessageQueue;

public static class RasputinMessageQueue
{
    private static ConnectionFactory? _factory;
    private static RasputinMessageQueueConfig? _config;
    private static IConnection? _connection;
    
    public static IConnection Connect(bool reuse = true)
    {
        if (_config == null)
        {
            _config = RasputinMessageQueueConfig.Load();
        }
        
        if (_factory == null)
        {
            _factory = new ConnectionFactory();
            _factory.UserName = _config.Username;
            _factory.Password = _config.Password;
            _factory.Port = (int)_config.Port;
            _factory.HostName = _config.Host;
            _factory.VirtualHost = _config.VirtualHost;
            _factory.ClientProvidedName = _config.ClientName;
        }

        // no matter what, we will always establish a reusable connection 
        // even if we dont return it the first time, that's fine
        if (_connection == null)
        {
            _connection = _factory.CreateConnection();
        }

        return reuse ? _connection : _factory.CreateConnection();
    }


    public static void Disconnect()
    {
        if (_connection != null)
        {
            _connection.Close();
            _connection = null;
        }
    }
}