using CurrencyConverter.Common.Exceptions;

namespace CurrencyConverter.ExchangeRatesProviderService.Models
{
    public class ExchangeRate
    {
        public ExchangeRate(string currency, string iso, decimal rate)
        {
            Currency = currency;
            Iso = iso;
            Rate = rate;

            if (rate <= 0)
            {
                throw new DomainException("Rate cannot be less or equal to 0.");
            }
        }

        public string Currency { get; }
        public string Iso { get; }
        public decimal Rate { get; }
    }
}
