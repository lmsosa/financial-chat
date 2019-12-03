using FinancialChat.Abstractions.ChatRoom;
using FinancialChat.Abstractions.HostingContext;
using FinancialChat.Abstractions.Persistence;
using FinancialChat.Application.Bot.Commands;
using FinancialChat.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialChat.Application.Chat.Commands
{
    public class SendMessageRequestHandler : IRequestHandler<SendMessageRequest>
    {
        private readonly IChatRoom _chatRoom;
        private readonly IChatUow _chatUow;
        private readonly IDateTime _dateTimeService;
        private readonly IMediator _mediator;

        public SendMessageRequestHandler(IChatRoom chatRoom,
                                         IChatUow chatUow,
                                         IDateTime dateTimeService,
                                         IMediator mediator)
        {
            _chatRoom = chatRoom;
            _chatUow = chatUow;
            _dateTimeService = dateTimeService;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(SendMessageRequest request, CancellationToken cancellationToken)
        {
            var chatMessage = new ChatMessage(request.UserName,
                                              request.Message,
                                              _dateTimeService.Now);
            if (!await _mediator.Send(new InterpretBotRequest(request.Message)))
            {
                await _chatUow.ChatMessages.CreateAsync(chatMessage);
                await _chatUow.Complete();
            }

            await _chatRoom.SendMessageToAll(chatMessage);

            return Unit.Value;
        }
    }
}
