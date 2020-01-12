using CurrencyConverter.Domain;

namespace CurrencyConverter.ExchangeRatesProviderService.Interfaces
{
    public interface IExchangeRatesProvider
    {
        decimal GetExchangeRate(Currency from, Currency to);
    }
}
