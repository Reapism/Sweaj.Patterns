using Ardalis.GuardClauses;
using Sweaj.Patterns.Data.ValueObjects;
using System.Text.RegularExpressions;

namespace Sweaj.Patterns.Tests
{
    public class AddressTests
    {
        [Theory]
        [InlineData("123 Main St", "Anytown", "NY", "12345", "USA", true)]
        [InlineData("123 Main St", "Anytown", "NY", "12345-6789", "USA", true)]
        [InlineData("123 Main St", "Anytown", "NY", "1234", "USA", false)]
        [InlineData("123 Main St", "Anytown", "NY", "123456789", "USA", false)]
        public void ValidatePostalCode_ReturnsExpectedResult(string street, string city, string state, string zip, string country, bool expected)
        {
            // Arrange
            var address = Address.Create(street, city, state, zip, country);

            // Act
            var result = Address.ValidatePostalCode(zip);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("123 Main St", "Anytown", "NY", "12345", "USA", "Anytown, NY")]
        [InlineData("123 Main St", "Anytown", "NY", "12345", "USA", "123 Main St, Anytown, NY, 12345, USA")]
        public void ToString_ReturnsExpectedResult(string street, string city, string state, string zip, string country, string expected)
        {
            // Arrange
            var address = Address.Create(street, city, state, zip, country);

            // Act
            var shortResult = address.ToString(AddressFormat.Short);
            var longResult = address.ToString(AddressFormat.Long);

            // Assert
            Assert.Equal(expected, shortResult);
            Assert.Equal(expected, longResult);
        }

        [Theory]
        [InlineData("123 Main St", "Anytown", "NY", "12345", "USA", "USA", true)]
        [InlineData("123 Main St", "Anytown", "NY", "12345", "USA", "Canada", false)]
        public void IsInCountry_ReturnsExpectedResult(string street, string city, string state, string zip, string country, string inputCountry, bool expected)
        {
            // Arrange
            var address = Address.Create(street, city, state, zip, country);

            // Act
            var result = address.IsInCountry(inputCountry);

            // Assert
            Assert.Equal(expected, result);
        }

    }
}