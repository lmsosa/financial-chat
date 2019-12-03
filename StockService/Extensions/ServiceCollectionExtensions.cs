using FinancialChat.Abstractions.StockService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;

namespace FinancialChat.StockService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStockService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IStockService, StockService>(c =>
            {
                c.BaseAddress = new Uri("https://stooq.com");
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));
            });
            return services;
        }
    }
}
