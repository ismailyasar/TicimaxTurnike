using RabbitMQ.Client;

namespace TicimaxTurnike.API.Services;

public class RabbitMQClientService
{
    private readonly ConnectionFactory _connectionFactory;
    private  IConnection _connection;
    private IModel _channel;
    
    public static string QueueName = "queue-add-entry";

    public RabbitMQClientService(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public IModel Connect()
    {
        _connection = _connectionFactory.CreateConnection();

        if (_channel is {IsOpen: true})
        {
            return _channel;
        }

        _channel = _connection.CreateModel();
        _channel.QueueDeclare(QueueName, true, false, false, null);
        return _channel;
    }

}