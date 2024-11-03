using RabbitMQ.Client;

namespace Rasputin.MessageQueue;

public static class MessageQueueClient
{
    private static ConnectionFactory? _factory;
    private static RasputinMessageQueueConfig? _config;
    
    private static IConnection Connect()
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
        
        return _factory.CreateConnection();
    }
}