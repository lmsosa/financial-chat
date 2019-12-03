using FinancialChat.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;

namespace FinancialChat.Persistence.Identity
{
    public class FinancialChatUser : IdentityUser, IUser
    {
    }
}
