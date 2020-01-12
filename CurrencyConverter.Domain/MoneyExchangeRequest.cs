using CurrencyConverter.Common.Exceptions;

namespace CurrencyConverter.Domain
{
    public class MoneyExchangeRequest
    {
        public MoneyExchangeRequest(Currency currencyFrom, Currency currencyTo, decimal amount)
        {
            CurrencyFrom = currencyFrom ?? throw new DomainException("CurrencyFrom cannot be null.");
            CurrencyTo = currencyTo ?? throw new DomainException("CurrencyTo cannot be null.");

            if (amount < 0)
            {
                throw new DomainException("Amount cannot be less than 0");
            }

            Amount = amount;
        }

        public Currency CurrencyFrom { get; private set; }

        public Currency CurrencyTo { get; private set; }

        public decimal Amount { get; private set; }

        // Example of correct user input: EUR/DKK 123
        public static MoneyExchangeRequest CreateFromUserInput(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                throw new DomainException("Incorrect user input.");
            }
            userInput = userInput.Trim();
            var splitUserInput = userInput.Split(' ');
            var currencies = splitUserInput[0].Split('/');

            if (splitUserInput.Length != 2 || currencies.Length != 2)
            {
                throw new DomainException("Incorrect user input.");
            }

            var success = decimal.TryParse(splitUserInput[1], out decimal amountToConvert);
            if (!success)
            {
                throw new DomainException("Incorrect amount.");
            }


            var currencyFrom = new Currency(currencies[0]);
            var currencyTo = new Currency(currencies[1]);
            var moneyExchangeRequest = new MoneyExchangeRequest(currencyFrom, currencyTo, amountToConvert);

            return moneyExchangeRequest;
        }
    }
}
