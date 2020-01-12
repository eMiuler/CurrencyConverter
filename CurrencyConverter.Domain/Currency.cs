using CurrencyConverter.Common.Exceptions;

namespace CurrencyConverter.Domain
{
    public class Currency
    {
        public Currency(string iso)
        {
            if (string.IsNullOrWhiteSpace(iso))
            {
                throw new DomainException("ISO cannot be null or empty");
            }

            Iso = iso.ToUpperInvariant();
        }

        public string Iso { get; private set; }
    }
}
