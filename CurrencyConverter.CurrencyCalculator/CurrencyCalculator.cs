using CurrencyConverter.Common.Exceptions;
using CurrencyConverter.CurrencyCalculatorService.Interfaces;
using CurrencyConverter.Domain;
using CurrencyConverter.ExchangeRatesProviderService.Interfaces;

namespace CurrencyConverter.CurrencyCalculatorService
{
    public class CurrencyCalculator : ICurrencyCalculator
    {
        private readonly IExchangeRatesProvider _xrProvider;

        public CurrencyCalculator(IExchangeRatesProvider xrProvider)
        {
            _xrProvider = xrProvider;
        }

        public decimal Convert(MoneyExchangeRequest moneyExchangeRequest)
        {
            if (moneyExchangeRequest is null)
            {
                throw new DomainException("MoneyExchangeRequest cannot be null");
            }

            if (moneyExchangeRequest.Amount == 0)
            {
                return 0;
            }

            var exchangeRate = _xrProvider
                .GetExchangeRate(moneyExchangeRequest.CurrencyFrom, moneyExchangeRequest.CurrencyTo);
            var convertedAmount = decimal.Multiply(moneyExchangeRequest.Amount, exchangeRate);

            return convertedAmount;
        }
    }
}
