using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TicimaxTurnike.API.Services;
using TicimaxTurnike.Data.Abstract;
using TicimaxTurnike.Entity;
using TicimaxTurnike.Entity.Dtos;

namespace TicimaxTurnike.API.BackgroundService;

public class LastEntryAddService:Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly RabbitMQClientService _rabbitMqClientService;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private IModel _channel;

    public LastEntryAddService(RabbitMQClientService rabbitMqClientService, IServiceScopeFactory serviceScopeFactory)
    {
        _rabbitMqClientService = rabbitMqClientService;
        _serviceScopeFactory = serviceScopeFactory;
    }
   
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _channel = _rabbitMqClientService.Connect();
        _channel.BasicQos(0,1,false);
        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);
        
        consumer.Received+= ConsumerOnReceived;
        return Task.CompletedTask;
    }

    private Task ConsumerOnReceived(object? sender, BasicDeliverEventArgs e)
    {
        var body = e.Body.ToArray();
        var lastEntryDetailJsonText = Encoding.UTF8.GetString(body);
        var lastEntryDetailDto = JsonSerializer.Deserialize<LastEntryDetailDto>(lastEntryDetailJsonText);
        
        
        //Db ye yaz
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var service = scope.ServiceProvider.GetRequiredService<ILastEntryDetailRepository>();
            service.AddOrUpdateLastEntry(lastEntryDetailDto);
        }
        
        _channel.BasicAck(e.DeliveryTag,false);
        return Task.CompletedTask;


    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Dispose();
        return base.StopAsync(cancellationToken);
    }
}