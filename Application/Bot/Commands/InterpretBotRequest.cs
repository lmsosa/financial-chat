using MediatR;

namespace FinancialChat.Application.Bot.Commands
{
    public class InterpretBotRequest : IRequest<bool>
    {
        public InterpretBotRequest(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
