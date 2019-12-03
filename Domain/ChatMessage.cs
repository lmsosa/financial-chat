using System;

namespace FinancialChat.Domain
{
    public class ChatMessage
    {
        public ChatMessage()
        {

        }

        public ChatMessage(string userName,
                           string message,
                           DateTime dateTime)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Message = message;
            DateTime = dateTime;
        }

        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public string UserName { get; set; }

        public string Message { get; set; }
    }
}
