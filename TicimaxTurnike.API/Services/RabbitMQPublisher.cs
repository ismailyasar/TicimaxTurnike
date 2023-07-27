using System.Text;
using RabbitMQ.Client;

namespace TicimaxTurnike.API.Services;

public class RabbitMQPublisher
{
    private readonly RabbitMQClientService _rabbitMqClientService;

    public RabbitMQPublisher(RabbitMQClientService rabbitMqClientService)
    {
        _rabbitMqClientService = rabbitMqClientService;
    }

    public void Publish(string message)
    {
        var channel = _rabbitMqClientService.Connect();
        
        var bodyByte = Encoding.UTF8.GetBytes(message);
        
        //Kalıcı mesajlar
        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        
        channel.BasicPublish(string.Empty, routingKey:RabbitMQClientService.QueueName, null, bodyByte);
    }
}