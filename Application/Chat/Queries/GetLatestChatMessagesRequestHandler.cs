using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinancialChat.Abstractions.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialChat.Application.Chat.Queries
{
    public class GetLatestChatMessagesRequestHandler : IRequestHandler<GetLatestChatMessagesRequest, List<ChatMessageDto>>
    {
        private readonly IChatUow _chatUow;
        private readonly IMapper _mapper;

        public GetLatestChatMessagesRequestHandler(IChatUow chatUow, IMapper mapper)
        {
            _chatUow = chatUow;
            _mapper = mapper;
        }

        public Task<List<ChatMessageDto>> Handle(GetLatestChatMessagesRequest request, CancellationToken cancellationToken)
        {
            var latestMessages = _chatUow.ChatMessages
                    .QueryAsync()
                    .ProjectTo<ChatMessageDto>(_mapper.ConfigurationProvider)
                    .OrderByDescending(x => x.DateTime)
                    .Take(50)
                    .ToList()
                    .OrderBy(x => x.DateTime)
                    .ToList();
            return Task.FromResult(latestMessages);
        }
    }
}
