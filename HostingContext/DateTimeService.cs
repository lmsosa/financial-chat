using FinancialChat.Abstractions.HostingContext;
using System;

namespace FinancialChat.HostingContext
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
