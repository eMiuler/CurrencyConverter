using CurrencyConverter.Common.Exceptions;
using CurrencyConverter.Domain;
using Shouldly;
using Xunit;

namespace CurrencyConverter.UnitTests.Domain
{
    public class CurrencyTests
    {
        [Theory]
        [InlineData("iso")]
        [InlineData("ISO")]
        public void CreateCurrency_ValidISO_ShouldSucceed(string iso)
        {
            //Arrange
            var expectedIso = iso.ToUpperInvariant();

            //Act
            var currency = new Currency(iso);

            //Assert
            currency.Iso.ShouldBe(expectedIso);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void CreateCurrency_InvalidISO_ShouldThrowException(string iso)
        {
            //Act && Assert
            var error = Should.Throw<DomainException>(() => new Currency(iso));
        }
    }
}
