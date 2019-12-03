using FinancialChat.Domain;
using FinancialChat.Persistence.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FinancialChat.Persistence
{
    public class FinancialChatDbContext : IdentityDbContext<FinancialChatUser>
    {
        public FinancialChatDbContext(DbContextOptions<FinancialChatDbContext> options)
        : base(options)
        {
        }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
