using FinancialChat.Abstractions.HostingContext;
using FinancialChat.Application.Chat.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FinancialChat.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;
        private readonly IDateTime _dateTime;

        public ChatHub(IMediator mediator, IDateTime dateTime)
        {
            _mediator = mediator;
            _dateTime = dateTime;
        }

        public Task SendMessageToAll(string message, string user)
        {
            return _mediator.Send(new SendMessageRequest(user, message));
        }
    }
}
