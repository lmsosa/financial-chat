using FinancialChat.Abstractions.MessageBroker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialChat.MessageBroker.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static IServiceScope _scope;
        private static StockRequestConsumer _stockRequestConsumer;


        public static IServiceCollection AddMessageBroker(this IServiceCollection services)
        {
            services.AddSingleton<IStockRequestSender, StockRequestProducer>();
            services.AddSingleton<StockRequestConsumer>();
            return services;
        }

        public static IApplicationBuilder UseMessageBroker(this IApplicationBuilder builder)
        {
            _scope = builder.ApplicationServices.CreateScope();
            _stockRequestConsumer = _scope.ServiceProvider.GetService<StockRequestConsumer>();

            var lifetime = builder.ApplicationServices.GetService<IApplicationLifetime>();

            lifetime.ApplicationStarted.Register(OnStarted);
            lifetime.ApplicationStopped.Register(OnStopped);

            return builder;

        }

        private static void OnStarted()
        {
            _stockRequestConsumer.StartAsync();
        }

        private static void OnStopped()
        {
            _stockRequestConsumer.StopAndDispose();
            _scope.Dispose();
        }
    }
}
