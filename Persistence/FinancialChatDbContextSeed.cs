using FinancialChat.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialChat.Persistence
{
    public static class FinancialChatDbContextSeed
    {
        public static async Task SeedAsync(UserManager<FinancialChatUser> userManager)
        {
            var defaultUser = new FinancialChatUser { UserName = "lmsosa@gmail.com", Email = "lmsosa@gmail.com" };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, "Chat123$");
            }

            var anotherUser = new FinancialChatUser { UserName = "jeancarlos@gmail.com", Email = "jeancarlos@gmail.com" };
            if (userManager.Users.All(u => u.Id != anotherUser.Id))
            {
                await userManager.CreateAsync(anotherUser, "Chat123$");
            }
        }
    }

}
