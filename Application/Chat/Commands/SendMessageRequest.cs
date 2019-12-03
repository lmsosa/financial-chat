using MediatR;

namespace FinancialChat.Application.Chat.Commands
{
    public class SendMessageRequest : IRequest
    {
        public SendMessageRequest(string userName, string message)
        {
            UserName = userName;
            Message = message;
        }

        public string UserName { get; private set; }
        public string Message { get; private set; }
    }
}
