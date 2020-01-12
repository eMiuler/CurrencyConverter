using CurrencyConverter.Common.Exceptions;
using CurrencyConverter.CurrencyCalculatorService.Interfaces;
using CurrencyConverter.Domain;
using System;
using System.Linq;

namespace CurrencyConverter.ConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            var diContainer = new DIContainer();
            var calculator = diContainer.GetService<ICurrencyCalculator>();
            string userInput = string.Empty;
#if DEBUG
            Console.WriteLine("Usage: <currencyFrom>/<currencyTo> <amount to exchange>");
            userInput = Console.ReadLine();
#else
            if (!args.Any() || args.Length != 2)
            {
                Console.WriteLine("Usage: Exchange <currencyFrom>/<currencyTo> <amount to exchange>");
                return;
            }
            userInput = $"{args[0]} {args[1]}";
#endif
            try
            {
                var moneyExchangeRequest = MoneyExchangeRequest.CreateFromUserInput(userInput);

                var convertedAmount = calculator.Convert(moneyExchangeRequest);

                Console.WriteLine($"{convertedAmount} {moneyExchangeRequest.CurrencyTo.Iso}");
            }
            catch (DomainException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong.");
                return;
            }
        }
    }
}
