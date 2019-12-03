﻿using FinancialChat.Abstractions.ChatRoom;
using FinancialChat.Abstractions.HostingContext;
using FinancialChat.Abstractions.StockService;
using FinancialChat.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialChat.Application.Bot.Commands
{
    public class FetchStockQuoteRequestHandler : IRequestHandler<FetchStockQuoteRequest>
    {
        private readonly IStockService _stockService;
        private readonly IChatRoom _chatRoom;
        private readonly IDateTime _dateTimeService;

        public FetchStockQuoteRequestHandler(IStockService stockService,
                                             IChatRoom chatRoom,
                                             IDateTime dateTimeService)
        {
            _stockService = stockService;
            _chatRoom = chatRoom;
            _dateTimeService = dateTimeService;
        }

        public async Task<Unit> Handle(FetchStockQuoteRequest request, CancellationToken cancellationToken)
        {
            var stockQuote = await _stockService.GetStockQuote(request.StockCode);

            await _chatRoom.SendMessageToAll(new ChatMessage("Financial Bot", stockQuote, _dateTimeService.Now));

            return Unit.Value;
        }
    }
}
