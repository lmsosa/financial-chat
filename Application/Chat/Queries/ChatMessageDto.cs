using AutoMapper;
using FinancialChat.Application.Common.Mappings;
using FinancialChat.Domain;
using System;

namespace FinancialChat.Application.Chat.Queries
{
    public class ChatMessageDto : IMapFrom<ChatMessage>
    {
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public string UserName { get; set; }

        public string Message { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(ChatMessage), GetType());
        }
    }
}