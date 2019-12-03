using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;

namespace FinancialChat.MessageBroker.RabbitMq
{
    public abstract class RabbitMqConsumer
    {
        private IConnection _connection;
        private IModel _channel;
        protected abstract string ExchangeName { get; }
        protected abstract string QueueName { get; }

        public RabbitMqConsumer(IConfiguration configuration)
        {
            var factory = new ConnectionFactory
            {
                HostName = configuration["MessageBroker:HostName"],
                UserName = configuration["MessageBroker:UserName"],
                Password = configuration["MessageBroker:Password"],
                Port = int.Parse(configuration["MessageBroker:Port"])
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            if (!string.IsNullOrEmpty(ExchangeName))
            {
                _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
            }
            _channel.QueueDeclare(QueueName, true, false, false, null);
            if (!string.IsNullOrEmpty(ExchangeName))
            {
                _channel.QueueBind(QueueName, ExchangeName, string.Empty);
            }
        }

        public Task StartAsync()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = System.Text.Encoding.UTF8.GetString(ea.Body);
                HandleMessage(content);
            };

            _channel.BasicConsume(QueueName, true, consumer);
            return Task.CompletedTask;
        }

        public void StopAndDispose()
        {
            if (_connection != null)
            {
                _connection.Close();
            }

            if (_channel != null && _channel.IsOpen)
            {
                _channel.Abort();
            }
            GC.SuppressFinalize(this);
        }

        protected abstract Task HandleMessage(string message);

    }
}
