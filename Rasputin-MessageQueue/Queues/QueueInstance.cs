using System.Diagnostics;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rasputin.MessageQueue.Models;

namespace Rasputin.MessageQueue.Queues;

public static class QueueInstance
{

    private static IConnection? _connection;
    private static IModel? _channel;

    private const string TARGET_EXCHANGE = "rasputin.direct";
    private const string TARGET_QUEUE = "rasputin.instances";
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
    
    public static void Publish(MessageMember message)
    {
        if (_channel == null)
        {
            Connect();
        }

        var serializedMessage = JsonSerializer.Serialize(message);
        var bytes = Encoding.UTF8.GetBytes(serializedMessage);
        var properties = _channel.CreateBasicProperties(); 
        properties.ContentType = "application/json";
        properties.DeliveryMode = 2;
        _channel.BasicPublish(TARGET_EXCHANGE, TARGET_ROUTING_KEY, properties, bytes);
    }


    public static string Subscribe(Func<MessageInstance?, Task> processCallback)
    {
        if (_channel == null)
        {
            Connect();
        }

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            // deserialize
            var body = ea.Body.ToArray();
            var message = JsonSerializer.Deserialize<MessageInstance>(body);
            
            // processs
            await processCallback(message);
            
            // yield
            await Task.Yield();
        };

        string consumerTag = _channel.BasicConsume(TARGET_QUEUE, true, consumer);
        return consumerTag;
    }

    public static MessageInstance? Pull()
    {
        if (_channel == null)
        {
            Connect();
        }

        var result = _channel.BasicGet(TARGET_QUEUE, true);
        if (result == null)
        {
            // no message
            return null;
        }
        else
        {
            var props = result.BasicProperties;
            var body = result.Body.ToArray();
            return JsonSerializer.Deserialize<MessageInstance>(body);
        }
    }
    
    
}