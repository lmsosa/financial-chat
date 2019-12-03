using FinancialChat.Domain;
using System.Threading.Tasks;

namespace FinancialChat.Abstractions.ChatRoom
{
    public interface IChatRoom
    {
        Task SendMessageToAll(ChatMessage chatMessage);
    }
}
