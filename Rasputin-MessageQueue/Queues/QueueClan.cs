using System.Diagnostics;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Rasputin.MessageQueue.Models;

namespace Rasputin.MessageQueue.Queues;

public static class QueueClan
{

    private static IConnection? _connection;
    private static IModel? _channel;

    private const string TARGET_EXCHANGE = "rasputin.direct";
    private const string TARGET_QUEUE = "rasputin.clans";
    private const string TARGET_ROUTING_KEY = TARGET_QUEUE; // since we are going direct, juse use the queue name
    public static void Connect()
    {
        if (_connection == null)
        {
            _connection = RasputinMessageQueue.Connect();
        }

        if (_channel == null)
        {
            _channel = _connection.CreateModel();
        }
        
        // declare queues and exchanges
        _channel.ExchangeDeclare(TARGET_EXCHANGE, ExchangeType.Direct, true);
        _channel.QueueDeclare(TARGET_QUEUE, true, false, false, null);
        _channel.QueueBind(TARGET_QUEUE,TARGET_EXCHANGE, TARGET_ROUTING_KEY, null);
    }

    public static void Disconnect()
    {
        if (_channel != null)
        {
            _channel.Close();
            _channel = null;
        }
    }
    
    public static void Publish(MessageClan clanMessage)
    {
        if (_channel == null)
        {
            QueueClan.Connect();
        }
        
        var serializedMessage = JsonSerializer.Serialize(clanMessage);
        var bytes = Encoding.UTF8.GetBytes(serializedMessage);
        var properties = _channel.CreateBasicProperties(); 
        properties.ContentType = "application/json";
        properties.DeliveryMode = 2;
        _channel.BasicPublish(TARGET_EXCHANGE, TARGET_ROUTING_KEY, properties, bytes);
    }
}