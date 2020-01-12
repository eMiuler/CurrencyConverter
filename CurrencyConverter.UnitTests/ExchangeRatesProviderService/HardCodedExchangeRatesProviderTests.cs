using CurrencyConverter.Common.Exceptions;
using CurrencyConverter.Domain;
using CurrencyConverter.ExchangeRatesProviderService;
using Shouldly;
using System.Linq;
using Xunit;

namespace CurrencyConverter.UnitTests.ExchangeRatesProviderService
{
    public class HardCodedExchangeRatesProviderTests : HardCodedExchangeRatesProvider
    {
        [Fact]
        public void GetExchangeRate_ShouldSucceed()
        {
            //Arrange
            var from = new Currency("nok");
            var to = new Currency("JPY");

            var expectedExchangeRate = decimal.Divide(_exchangeRates.First(x => x.Iso == from.Iso).Rate,
                _exchangeRates.First(x => x.Iso == to.Iso).Rate);

            //Act
            var exchangeRate = GetExchangeRate(from, to);

            //Assert
            exchangeRate.ShouldBe(expectedExchangeRate);
        }

        [Fact]
        public void GetExchangeRate_FromAndToAreSameCurrencies_ShouldReturnOne()
        {
            //Arrange
            var from = new Currency("nok");
            var to = new Currency("nok");

            var expectedExchangeRate = 1;

            //Act
            var exchangeRate = GetExchangeRate(from, to);

            //Assert
            exchangeRate.ShouldBe(expectedExchangeRate);
        }

        [Fact]
        public void GetExchangeRate_ConvertToMainCurrency_ShouldSucceed()
        {
            //Arrange
            var from = new Currency("eur");
            var to = new Currency("dkk");

            var expectedExchangeRate = _exchangeRates.First(x => x.Iso == from.Iso).Rate / 100;

            //Act
            var exchangeRate = GetExchangeRate(from, to);

            //Assert
            exchangeRate.ShouldBe(expectedExchangeRate);
        }

        [Fact]
        public void GetExchangeRate_FromIsNull_ShouldThrowException()
        {
            //Arrange
            var from = null as Currency;
            var to = new Currency("dkk");

            //Act && Assert
            var message = Should.Throw<DomainException>(() => GetExchangeRate(from, to)).Message;
            message.ShouldBe("Currency cannot be null");
        }

        [Fact]
        public void GetExchangeRate_ToIsNull_ShouldThrowException()
        {
            //Arrange
            var from = new Currency("dkk");
            var to = null as Currency;

            //Act && Assert
            var message = Should.Throw<DomainException>(() => GetExchangeRate(from, to)).Message;
            message.ShouldBe("Currency cannot be null");
        }

        [Fact]
        public void GetExchangeRate_FromCurrencyDoesntExist_ShouldThrowException()
        {
            //Arrange
            var from = new Currency("doesntexist");
            var to = new Currency("dkk");

            //Act && Assert
            var message = Should.Throw<DomainException>(() => GetExchangeRate(from, to)).Message;
            message.ShouldBe($"Currency {from.Iso} not found.");
        }

        [Fact]
        public void GetExchangeRate_ToCurrencyDoesntExist_ShouldThrowException()
        {
            //Arrange
            var from = new Currency("dkk");
            var to = new Currency("doesntexist");

            //Act && Assert
            var message = Should.Throw<DomainException>(() => GetExchangeRate(from, to)).Message;
            message.ShouldBe($"Currency {to.Iso} not found.");
        }
    }
}
