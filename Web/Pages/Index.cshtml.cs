using FinancialChat.Application.Chat.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialChat.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public List<ChatMessageDto> ChatMessages { get; set; }

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task OnGet()
        {
            ChatMessages = await _mediator.Send(new GetLatestChatMessagesRequest());
        }
    }
}
