using System.Threading.Tasks;

namespace FinancialChat.Abstractions.MessageBroker
{
    public interface IStockRequestSender
    {
        Task SendStockRequest(string stockCode);
    }
}
