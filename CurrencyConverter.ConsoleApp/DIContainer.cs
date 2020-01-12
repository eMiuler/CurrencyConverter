using CurrencyConverter.CurrencyCalculatorService;
using CurrencyConverter.CurrencyCalculatorService.Interfaces;
using CurrencyConverter.ExchangeRatesProviderService;
using CurrencyConverter.ExchangeRatesProviderService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CurrencyConverter.ConsoleApp
{
    public class DIContainer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public DIContainer()
        {
            var builder = new ConfigurationBuilder();
            _configuration = builder.Build();

            var services = ConfigureServices();

            _serviceProvider = services.BuildServiceProvider();
        }

        private IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IExchangeRatesProvider, HardCodedExchangeRatesProvider>();
            services.AddSingleton<ICurrencyCalculator, CurrencyCalculator>();

            return services;
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
