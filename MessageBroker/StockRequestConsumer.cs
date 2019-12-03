using FinancialChat.Application.Bot.Commands;
using FinancialChat.MessageBroker.RabbitMq;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FinancialChat.MessageBroker
{
    public class StockRequestConsumer : RabbitMqConsumer
    {
        private const string FinancialStocksQueueName = "Financial.Stocks";
        private readonly IMediator _mediator;

        public StockRequestConsumer(IConfiguration configuration,
                                    IMediator mediator) : base(configuration)
        {
            _mediator = mediator;
        }

        protected override string ExchangeName { get => string.Empty; }
        protected override string QueueName { get => FinancialStocksQueueName; }

        protected override async Task HandleMessage(string message)
        {
            await _mediator.Send(new FetchStockQuoteRequest(message));
        }
    }
}
