using CurrencyConverter.Common.Exceptions;
using CurrencyConverter.Domain;
using CurrencyConverter.ExchangeRatesProviderService.Interfaces;
using CurrencyConverter.ExchangeRatesProviderService.Models;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyConverter.ExchangeRatesProviderService
{
    public class HardCodedExchangeRatesProvider : IExchangeRatesProvider
    {
        protected List<ExchangeRate> _exchangeRates;

        public HardCodedExchangeRatesProvider()
        {
            _exchangeRates = new List<ExchangeRate>
            {
                new ExchangeRate("Danish kroner", "DKK", 100m),
                new ExchangeRate("Euro", "EUR", 743.94m),
                new ExchangeRate("Amerikanske dollar", "USD", 663.11m),
                new ExchangeRate("Britiske pund", "GBP", 852.85m),
                new ExchangeRate("Svenske kroner", "SEK", 76.1m),
                new ExchangeRate("Norske kroner", "NOK", 78.4m),
                new ExchangeRate("Schweiziske franc", "CHF", 683.85m),
                new ExchangeRate("Japanske yen", "JPY", 5.9740m),
            };
        }

        public decimal GetExchangeRate(Currency from, Currency to)
        {
            if (from is null || to is null)
            {
                throw new DomainException("Currency cannot be null");
            }

            var fromToMainXR = _exchangeRates.FirstOrDefault(xr => xr.Iso == from.Iso)
                ?? throw new DomainException($"Currency {from.Iso} not found.");

            var toToMainXR = _exchangeRates.FirstOrDefault(xr => xr.Iso == to.Iso)
                ?? throw new DomainException($"Currency {to.Iso} not found.");

            var exchangeRate = decimal.Divide(fromToMainXR.Rate, toToMainXR.Rate);

            return exchangeRate;
        }
    }
}
