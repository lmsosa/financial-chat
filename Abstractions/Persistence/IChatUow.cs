using FinancialChat.Domain;
using System.Threading.Tasks;

namespace FinancialChat.Abstractions.Persistence
{
    public interface IChatUow
    {
        IRepository<ChatMessage> ChatMessages { get; }
        Task Complete();
    }
}
