using MediatR;
using System.Collections.Generic;

namespace FinancialChat.Application.Chat.Queries
{
    public class GetLatestChatMessagesRequest : IRequest<List<ChatMessageDto>>
    {
    }
}
