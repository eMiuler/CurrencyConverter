using CurrencyConverter.Common.Exceptions;
using CurrencyConverter.CurrencyCalculatorService;
using CurrencyConverter.CurrencyCalculatorService.Interfaces;
using CurrencyConverter.Domain;
using CurrencyConverter.ExchangeRatesProviderService.Interfaces;
using Moq;
using Shouldly;
using Xunit;

namespace CurrencyConverter.UnitTests.CurrencyCalculatorService
{
    public class CurrencyCalculatorTests
    {
        private readonly Mock<IExchangeRatesProvider> _xrProviderMock;
        private readonly ICurrencyCalculator _currencyCalculator;

        public CurrencyCalculatorTests()
        {
            _xrProviderMock = new Mock<IExchangeRatesProvider>();

            _currencyCalculator = new CurrencyCalculator(
                _xrProviderMock.Object);
        }

        [Fact]
        public void Convert_MoneyExchangeRequestIsNull_ShouldThrowException()
        {
            //Arrange
            var moneyExchangeRequest = null as MoneyExchangeRequest;

            //Act && Assert
            var message = Should.Throw<DomainException>(() =>
                _currencyCalculator.Convert(moneyExchangeRequest)).Message;

            message.ShouldBe("MoneyExchangeRequest cannot be null");
        }

        [Fact]
        public void Convert_AmountIsZero_ShouldReturnZero()
        {
            //Arrange
            var currencyFrom = new Currency("dkk");
            var currencyTo = new Currency("eur");
            var moneyExchangeRequest = new MoneyExchangeRequest(currencyFrom, currencyTo, 0);

            //Act
            var result = _currencyCalculator.Convert(moneyExchangeRequest);

            //Assert
            result.ShouldBe(0);
        }

        [Fact]
        public void Convert_ShouldSucceed()
        {
            //Arrange
            var currencyFrom = new Currency("dkk");
            var currencyTo = new Currency("eur");
            var amount = 19.51m;
            var moneyExchangeRequest = new MoneyExchangeRequest(currencyFrom, currencyTo, amount);
            var exchangeRate = 2.57m;
            var expectedResult = decimal.Multiply(amount, exchangeRate);

            _xrProviderMock.Setup(x => x.GetExchangeRate(currencyFrom, currencyTo))
                .Returns(exchangeRate);

            //Act
            var result = _currencyCalculator.Convert(moneyExchangeRequest);

            //Assert
            result.ShouldBe(expectedResult);
        }
    }
}
