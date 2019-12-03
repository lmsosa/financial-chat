using FinancialChat.Abstractions.MessageBroker;
using FinancialChat.MessageBroker.RabbitMq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FinancialChat.MessageBroker
{
    public class StockRequestProducer : RabbitMqProducer, IStockRequestSender
    {
        private const string FinancialStocksQueueName = "Financial.Stocks";

        public StockRequestProducer(IConfiguration configuration) : base(configuration)
        { }

        protected override string ExchangeName { get => string.Empty; }
        protected override string QueueName { get => FinancialStocksQueueName; }

        public Task SendStockRequest(string stockCode)
        {
            base.Produce(stockCode);
            return Task.CompletedTask;
        }
    }
}
