using FinancialChat.Abstractions.MessageBroker;
using MediatR;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialChat.Application.Bot.Commands
{
    public class InterpretBotRequestHandler : IRequestHandler<InterpretBotRequest, bool>
    {
        private readonly IStockRequestSender _stockRequestSender;

        public InterpretBotRequestHandler(IStockRequestSender stockRequestSender)
        {
            _stockRequestSender = stockRequestSender;
        }

        public Task<bool> Handle(InterpretBotRequest request, CancellationToken cancellationToken)
        {
            if (Regex.IsMatch(request.Message, @"(\/stock=)\w+"))
            {
                var stockCode = request.Message.Split('=')[1];

                _stockRequestSender.SendStockRequest(stockCode);

                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
