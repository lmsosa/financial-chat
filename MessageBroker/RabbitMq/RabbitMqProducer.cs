using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.MessageBroker.RabbitMq
{
    public abstract class RabbitMqProducer
    {
        private IConnection _connection;
        private IModel _channel;

        protected abstract string ExchangeName { get; }
        protected abstract string QueueName { get; }

        public RabbitMqProducer(IConfiguration configuration)
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
            _channel.QueueDeclare(queue: QueueName,
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
            if (!string.IsNullOrEmpty(ExchangeName))
            {
                _channel.QueueBind(QueueName, ExchangeName, string.Empty);
            }
        }

        public void Disconnect()
        {
            _connection.Close();
        }

        public Task Produce(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: ExchangeName,
                     routingKey: QueueName,
                     basicProperties: null,
                     body: body);
            return Task.CompletedTask;
        }
    }

}
