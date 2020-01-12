using CurrencyConverter.Common.Exceptions;
using CurrencyConverter.Domain;
using Shouldly;
using System;
using Xunit;

namespace CurrencyConverter.UnitTests.Domain
{
    public class MoneyExchangeRequestTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(15)]
        public void CreateMoneyExchangeRequest_ValidData_ShouldSucceed(int amount)
        {
            //Arrange
            var currencyFrom = GetValidCurrency();
            var currencyTo = GetValidCurrency();

            //Act
            var moneyExchangeRequest = new MoneyExchangeRequest(currencyFrom, currencyTo, amount);

            //Assert
            moneyExchangeRequest.CurrencyFrom.Iso.ShouldBe(currencyFrom.Iso);
            moneyExchangeRequest.CurrencyTo.Iso.ShouldBe(currencyTo.Iso);
            moneyExchangeRequest.Amount.ShouldBe(amount);
        }

        [Fact]
        public void CreateMoneyExchangeRequest_CurrencyFromIsNull_ShouldThrowException()
        {
            //Arrange
            var currencyFrom = null as Currency;
            var currencyTo = GetValidCurrency();
            var amount = 200.5m;

            //Act && Assert
            var error = Should.Throw<DomainException>(() =>
                new MoneyExchangeRequest(currencyFrom, currencyTo, amount)).Message;

            error.ShouldBe("CurrencyFrom cannot be null.");
        }

        [Fact]
        public void CreateMoneyExchangeRequest_CurrencyToIsNull_ShouldThrowException()
        {
            //Arrange
            var currencyFrom = GetValidCurrency();
            var currencyTo = null as Currency;
            var amount = 200.5m;

            //Act && Assert
            var error = Should.Throw<DomainException>(() =>
                new MoneyExchangeRequest(currencyFrom, currencyTo, amount)).Message;

            error.ShouldBe("CurrencyTo cannot be null.");
        }

        [Fact]
        public void CreateMoneyExchangeRequest_AmountIsNegative_ShouldThrowException()
        {
            //Arrange
            var currencyFrom = GetValidCurrency();
            var currencyTo = GetValidCurrency();
            var amount = -200.5m;

            //Act && Assert
            var error = Should.Throw<DomainException>(() =>
                new MoneyExchangeRequest(currencyFrom, currencyTo, amount)).Message;

            error.ShouldBe("Amount cannot be less than 0");
        }

        [Theory]
        [InlineData("GBP/USD 1.51")]
        [InlineData("GBP/USD 1.51      ")]
        [InlineData("Aa/B 0.11")]
        [InlineData("kk/kk 1000")]
        public void CreateFromUserInput_InputIsValid_ShouldSucceed(string userInput)
        {
            //Arrange
            var splitUserInput = userInput.Split(' ');
            var expectedCurrencyFrom = splitUserInput[0].Split('/')[0].ToUpperInvariant();
            var expectedCurrencyTo = splitUserInput[0].Split('/')[1].ToUpperInvariant();
            var expectedAmount = decimal.Parse(splitUserInput[1]);

            //Act
            var moneyExchangeRequest = MoneyExchangeRequest.CreateFromUserInput(userInput);

            //Assert
            moneyExchangeRequest.CurrencyFrom.Iso.ShouldBe(expectedCurrencyFrom);
            moneyExchangeRequest.CurrencyTo.Iso.ShouldBe(expectedCurrencyTo);
            moneyExchangeRequest.Amount.ShouldBe(expectedAmount);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("15.1")]
        [InlineData("/b 15.2")]
        [InlineData("a/ 15.3")]
        [InlineData("a/b ")]
        [InlineData("a\b 16")]
        [InlineData("a/b aaa")]
        [InlineData("a/b aaa 15")]
        [InlineData("a/b 15 aaaa")]
        [InlineData("a/b/c aaa 15")]
        public void CreateFromUserInput_InputIsInvalid_ShouldThrowException(string userInput)
        {
            //Act && Assert
            Should.Throw<DomainException>(() => MoneyExchangeRequest.CreateFromUserInput(userInput));
        }

        private Currency GetValidCurrency()
        {
            var currency = new Currency(Guid.NewGuid().ToString());

            return currency;
        }
    }
}
