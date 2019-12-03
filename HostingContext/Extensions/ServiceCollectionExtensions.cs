using FinancialChat.Abstractions.HostingContext;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialChat.HostingContext.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHostingContext(this IServiceCollection services)
        {
            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
