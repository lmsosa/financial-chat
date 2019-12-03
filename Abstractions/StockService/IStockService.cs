using System.Threading.Tasks;

namespace FinancialChat.Abstractions.StockService
{
    public interface IStockService
    {
        Task<string> GetStockQuote(string stockCode);
    }
}
