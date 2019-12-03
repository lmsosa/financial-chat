using MediatR;

namespace FinancialChat.Application.Bot.Commands
{
    public class FetchStockQuoteRequest : IRequest
    {
        public FetchStockQuoteRequest(string stockCode)
        {
            StockCode = stockCode;
        }

        public string StockCode { get; }
    }
}
