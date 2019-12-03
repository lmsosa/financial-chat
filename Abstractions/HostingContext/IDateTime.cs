using System;

namespace FinancialChat.Abstractions.HostingContext
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
