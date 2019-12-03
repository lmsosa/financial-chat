using FinancialChat.Abstractions.ChatRoom;
using FinancialChat.Domain;
using FinancialChat.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FinancialChat.SignalR
{
    public class ChatRoom : IChatRoom
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatRoom(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task SendMessageToAll(ChatMessage chatMessage)
        {
            return _hubContext.Clients.All.SendAsync("ReceiveMessage",
                                                     chatMessage.Message,
                                                     chatMessage.UserName,
                                                     chatMessage.DateTime.ToString("dd/MM/yyyy HH:mm"));
        }
    }
}
