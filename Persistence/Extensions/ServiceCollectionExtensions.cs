using FinancialChat.Abstractions.Persistence;
using FinancialChat.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialChat.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FinancialChatDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(FinancialChatDbContext).Assembly.FullName)));

            services.AddScoped<IChatUow, ChatUnitOfWork>();

            services.AddDefaultIdentity<FinancialChatUser>()
                .AddEntityFrameworkStores<FinancialChatDbContext>();

            return services;
        }
    }
}
