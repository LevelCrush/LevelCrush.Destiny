using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rasputin.MessageQueue.Models;

namespace Rasputin.MessageQueue.Queues;

public class QueueBase<MessageModel> where MessageModel : class
{
    protected static IConnection? _connection;
    protected static IModel? _channel;


    private  string _targetExchange = "rasputin.direct";
    private  string _targetQueue = "rasputin.queue";
    private  string _targetRoutingKey = "rasputin.routing_key"; // direct mode

    public QueueBase(string targetExchange, string targetQueue, string routingKey)
    {
        _targetExchange = targetExchange;
        _targetQueue = targetQueue;
        _targetRoutingKey = routingKey;
    }
    
    
    public void Connect()
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
        _channel.ExchangeDeclare(_targetExchange, ExchangeType.Direct, true);
        _channel.QueueDeclare(_targetQueue, true, false, false, null);
        _channel.QueueBind(_targetQueue,_targetExchange, _targetRoutingKey, null);
    }


    public void Disconnect()
    {
        if (_channel != null)
        {
            _channel.Close();
            _channel = null;
        }
    }
    
    public  void Publish(MessageModel message)
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
        _channel.BasicPublish(_targetExchange, _targetRoutingKey, properties, bytes);
    }


    public  string Subscribe(Func<MessageModel?, Task> processCallback)
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
            var message = JsonSerializer.Deserialize<MessageModel>(body);
            
            // processs
            await processCallback(message);
            
            // yield
            await Task.Yield();
        };

        string consumerTag = _channel.BasicConsume(_targetQueue, true, consumer);
        return consumerTag;
    }

    public  MessageModel? Pull()
    {
        if (_channel == null)
        {
            Connect();
        }

        var result = _channel.BasicGet(_targetQueue, true);
        if (result == null)
        {
            // no message
            return null;
        }
        else
        {
            var props = result.BasicProperties;
            var body = result.Body.ToArray();
            return JsonSerializer.Deserialize<MessageModel>(body);
        }
    }
    
}