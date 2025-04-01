using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportService.Core.Interfaces;
using ReportService.Core.Models;

namespace ReportService.Infrastructure.Consumer.Services
{
    public class ReportServiceConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceScopeFactory _scopeFactory; // IServiceScopeFactory ekledik

        public ReportServiceConsumer(IServiceScopeFactory scopeFactory) 
        {
            _scopeFactory = scopeFactory;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("report_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, e) =>
            {
                using (var scope = _scopeFactory.CreateScope()) // Scoped servisi çözümlemek için scope açtık
                {
                    var reportService = scope.ServiceProvider.GetRequiredService<IReportService>();

                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var reportRequest = JsonSerializer.Deserialize<ReportRequest>(message);

                    await reportService.ProcessReportRequestAsync(reportRequest); // Scoped servis burada çağrılıyor

                    _channel.BasicAck(e.DeliveryTag, false);
                }
            };

            _channel.BasicConsume(queue: "report_queue", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
