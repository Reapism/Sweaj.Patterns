using Sweaj.Patterns.Data.ValueObjects;

namespace Sweaj.Patterns.Tests.Data.ValueObjects
{
    public class MoneyTests
    {
        [Theory]
        [InlineData(100, 50, "USD", 2, 150)]
        [InlineData(200, 100, "USD", 2, 300)]
        public void Money_Addition_SameCurrency_ShouldSucceed(decimal value1, decimal value2, string currency, int decimalPlaces, decimal expectedValue)
        {
            var money1 = new Money(value1, currency, decimalPlaces);
            var money2 = new Money(value2, currency, decimalPlaces);
            var result = money1 + money2;

            Assert.Equal(expectedValue, result.Value);
            Assert.Equal(currency, result.Currency);
        }

        [Theory]
        [InlineData(100, "USD", 50, "EUR")]
        [InlineData(200, "USD", 300, "GBP")]
        public void Money_Addition_DifferentCurrencies_ShouldThrowException(decimal value1, string currency1, decimal value2, string currency2)
        {
            var money1 = new Money(value1, currency1, 2);
            var money2 = new Money(value2, currency2, 2);

            Assert.Throws<ArgumentException>(() => money1 + money2);
        }

        [Theory]
        [InlineData(100, 50, "USD", 2, 50)]
        [InlineData(300, 100, "USD", 2, 200)]
        public void Money_Subtraction_SameCurrency_ShouldSucceed(decimal value1, decimal value2, string currency, int decimalPlaces, decimal expectedValue)
        {
            var money1 = new Money(value1, currency, decimalPlaces);
            var money2 = new Money(value2, currency, decimalPlaces);
            var result = money1 - money2;

            Assert.Equal(expectedValue, result.Value);
            Assert.Equal(currency, result.Currency);
        }

        [Theory]
        [InlineData(100, "USD", 50, "EUR")]
        [InlineData(200, "USD", 100, "GBP")]
        public void Money_Subtraction_DifferentCurrencies_ShouldThrowException(decimal value1, string currency1, decimal value2, string currency2)
        {
            var money1 = new Money(value1, currency1, 2);
            var money2 = new Money(value2, currency2, 2);

            Assert.Throws<ArgumentException>(() => money1 - money2);
        }

        [Theory]
        [InlineData(50, 100, "USD", 2)]
        [InlineData(100, 200, "USD", 2)]
        public void Money_Subtraction_NegativeResult_ShouldThrowException(decimal value1, decimal value2, string currency, int decimalPlaces)
        {
            var money1 = new Money(value1, currency, decimalPlaces);
            var money2 = new Money(value2, currency, decimalPlaces);

            Assert.Throws<InvalidOperationException>(() => money1 - money2);
        }

        [Theory]
        [InlineData(100, 50, "USD", 2, true)]
        [InlineData(100, 100, "USD", 2, false)]
        public void Money_Comparison_GreaterThan_ShouldSucceed(decimal value1, decimal value2, string currency, int decimalPlaces, bool expectedResult)
        {
            var money1 = new Money(value1, currency, decimalPlaces);
            var money2 = new Money(value2, currency, decimalPlaces);

            Assert.Equal(expectedResult, money1 > money2);
        }

        [Theory]
        [InlineData(50, 100, "USD", 2, true)]
        [InlineData(100, 100, "USD", 2, false)]
        public void Money_Comparison_LessThan_ShouldSucceed(decimal value1, decimal value2, string currency, int decimalPlaces, bool expectedResult)
        {
            var money1 = new Money(value1, currency, decimalPlaces);
            var money2 = new Money(value2, currency, decimalPlaces);

            Assert.Equal(expectedResult, money1 < money2);
        }

        [Theory]
        [InlineData(100, 100, "USD", 2, true)]
        [InlineData(100, 200, "USD", 2, false)]
        public void Money_Equality_ShouldSucceed(decimal value1, decimal value2, string currency, int decimalPlaces, bool expectedResult)
        {
            var money1 = new Money(value1, currency, decimalPlaces);
            var money2 = new Money(value2, currency, decimalPlaces);

            Assert.Equal(expectedResult, money1 == money2);
        }

        [Theory]
        [InlineData(100, 0)]
        [InlineData(200, 0)]
        public void Money_DivisionByZero_ShouldThrowException(decimal value1, decimal value2)
        {
            var money = new Money(value1, "USD", 2);

            Assert.Throws<ArgumentException>(() => money / value2);
        }

        [Theory]
        [InlineData(100, "USD", 2, "100.00 USD")]
        [InlineData(200.50, "USD", 2, "200.50 USD")]
        public void Money_ToString_ShouldReturnFormattedString(decimal value, string currency, int decimalPlaces, string expectedString)
        {
            var money = new Money(value, currency, decimalPlaces);
            var result = money.ToString();

            Assert.Equal(expectedString, result);
        }
    }

}
