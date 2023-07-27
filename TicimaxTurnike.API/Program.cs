using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using TicimaxTurnike.API.BackgroundService;
using TicimaxTurnike.API.Mappings;
using TicimaxTurnike.API.Services;
using TicimaxTurnike.Data.Abstract;
using TicimaxTurnike.Data.Concrete;
using TicimaxTurnike.Data.Concrete.EntityFramework;
using TicimaxTurnike.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql"));
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior",true);
});

builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped<IEntryRepository, EntryRepository>();
builder.Services.AddScoped<ILastEntryDetailRepository, LastEntryDetailRepository>();

builder.Services.AddAutoMapper(typeof(EntryProfile));

//Rabbit MQ
builder.Services.AddSingleton(sp => new ConnectionFactory()
{
    HostName = builder.Configuration.GetSection("RabbitMQConnectionInfo").GetSection("Host").Value,
    UserName = builder.Configuration.GetSection("RabbitMQConnectionInfo").GetSection("Username").Value,
    Password= builder.Configuration.GetSection("RabbitMQConnectionInfo").GetSection("Password").Value,
    DispatchConsumersAsync = true
    
});
builder.Services.AddSingleton<RabbitMQClientService>();
builder.Services.AddSingleton<RabbitMQPublisher>();
builder.Services.AddHostedService<LastEntryAddService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();