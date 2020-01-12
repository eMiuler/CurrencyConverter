using CurrencyConverter.Domain;

namespace CurrencyConverter.CurrencyCalculatorService.Interfaces
{
    public interface ICurrencyCalculator
    {
        decimal Convert(MoneyExchangeRequest moneyExchangeRequest);
    }
}
