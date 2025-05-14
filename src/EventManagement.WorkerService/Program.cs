using Core.CrossCuttingConcerns.Logging.Extensions;
using Core.Infrastructure.Messaging.RabbitMQ.Interfaces;
using EventManagement.Infrastructure;
using EventManagement.Infrastructure.Messaging.RabbitMQ;
using EventManagement.WorkerService.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddLoggingServices();
builder.Services.AddSingleton<IRabbitMQConsumer, RabbitMQConsumer>();

builder.Services.AddHostedService<ReportWorker>();

var host = builder.Build();
host.Run();
