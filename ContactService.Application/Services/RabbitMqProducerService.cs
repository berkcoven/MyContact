using ContactService.Core.Interfaces.Services;
using RabbitMQ.Client;
using System;
using System.Text;

namespace ContactService
{
    public class RabbitMqProducerService :IRabbitMqProducerService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName = "report_queue";

        public RabbitMqProducerService()
        {
            // RabbitMQ'ya bağlanmak için bağlantı fabrikasını oluşturuyoruz.
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",  // RabbitMQ sunucusunun adresi
                Port = 5672,            // RabbitMQ'nun varsayılan portu
                UserName = "guest",      // Varsayılan kullanıcı adı
                Password = "guest"       // Varsayılan şifre
            };

            // Bağlantı oluşturuluyor
            _connection = factory.CreateConnection();

            // Kanal oluşturuluyor (İletişim kanalı)
            _channel = _connection.CreateModel();
        }

        public void PublishMessage(string message)
        {
            // Kuyruğu tanımlıyoruz
            _channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // Mesajı byte dizisine çeviriyoruz
            var body = Encoding.UTF8.GetBytes(message);

            // Mesajı kuyruktan yayınlıyoruz
            _channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine($" [x] Sent: {message}");
        }

        // Bağlantıyı ve kanalı kapatıyoruz
        public void Close()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
