using Alg = Sweaj.Patterns.Math.MathExt.Algebra;
using System.Numerics;

namespace Sweaj.Patterns.Tests.Math.Algebra
{
    public class FactorialTests
    {
        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 6)]
        [InlineData(4, 24)]
        [InlineData(5, 120)]
        [InlineData(10, 3628800)]
        public void Factorial_ValidInputs_ReturnsExpectedResult(int input, long expected)
        {
            // Act
            BigInteger result = Alg.Factorial(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Factorial_NegativeInput_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Alg.Factorial(-1));
        }

        [Fact]
        public void Factorial_LargeInput_DoesNotOverflow()
        {
            // Arrange
            int input = 20;
            BigInteger expected = 2432902008176640000;

            // Act
            BigInteger result = Alg.Factorial(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("0", "1")]
        [InlineData("1", "1")]
        [InlineData("2", "2")]
        [InlineData("3", "6")]
        [InlineData("4", "24")]
        [InlineData("5", "120")]
        [InlineData("10", "3628800")]
        [InlineData("20", "2432902008176640000")]
        public void FactorialBig_ValidInputs_ReturnsExpectedResult(string inputStr, string expectedStr)
        {
            // Arrange
            BigInteger input = BigInteger.Parse(inputStr);
            BigInteger expected = BigInteger.Parse(expectedStr);

            // Act
            BigInteger result = Alg.FactorialBig(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FactorialBig_NegativeInput_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            BigInteger negativeInput = new BigInteger(-5);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Alg.FactorialBig(negativeInput));
        }

        [Theory]
        [InlineData("30", "265252859812191058636308480000000")]
        public void FactorialBig_LargeInput_ProducesExpectedLengthResult(string inputValue, string expectedResult)
        {
            // Arrange
            BigInteger input = BigInteger.Parse(inputValue);
            BigInteger expected = BigInteger.Parse("265252859812191058636308480000000");

            // Act
            BigInteger result = Alg.FactorialBig(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
