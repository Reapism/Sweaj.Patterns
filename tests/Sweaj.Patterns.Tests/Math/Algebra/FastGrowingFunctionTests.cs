using static Sweaj.Patterns.Math.MathExt.Algebra;
using System.Numerics;

namespace Sweaj.Patterns.Tests.Math.Algebra
{
    public class FastGrowingFunctionTests
    {
        [Theory]
        [InlineData(0, "0", "1")] // n + 1
        [InlineData(0, "1", "2")] // n + 1
        [InlineData(0, "2", "3")] // n + 1
        [InlineData(0, "3", "4")] // n + 1
        [InlineData(1, "0", "0")] // 2n
        [InlineData(1, "1", "2")] // 2n
        [InlineData(1, "2", "4")] // 2n
        [InlineData(1, "3", "6")] // 2n
        [InlineData(2, "0", "0")] // 2^n * n
        [InlineData(2, "1", "2")] // 2^n * n
        [InlineData(2, "2", "8")] // 2^n * n
        [InlineData(2, "3", "24")] // 2^n * n
        [InlineData(3, "0", "0")] // 2^2^n * n * 2^n * n
        [InlineData(3, "1", "2")] // 2^2^n * n * 2^n * n
        [InlineData(3, "2", "2048")] // 2^2^n * n * 2^n * n
        [InlineData(3, "3", "WAYTOBIG.", Skip = "This value has 121M digits")] // 2^2^n * n * 2^n * n
        [InlineData(4, "0", "0")] // 2^2^n * n * 2^n * n
        [InlineData(4, "1", "2")] // 2^2^n * n * 2^n * n
        [InlineData(4, "2", "WAYTOBIG.", Skip = "This value has 121M digits")] // 2^2^n * n * 2^n * n
        public void FastGrowingFunction_ValidInputs_ReturnsExpectedResult(int index, string inputStr, string expectedStr)
        {
            // Arrange
            BigInteger input = BigInteger.Parse(inputStr);
            BigInteger expected = BigInteger.Parse(expectedStr);

            // Act
            BigInteger result = FastGrowingFunction(index, input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1, "0")]
        [InlineData(0, "-1")]
        [InlineData(-2, "-3")]
        public void FastGrowingFunction_NegativeArguments_ThrowsArgumentOutOfRangeException(int index, string inputStr)
        {
            // Arrange
            BigInteger input = BigInteger.Parse(inputStr);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => FastGrowingFunction(index, input));
        }

        [Fact]
        public void FastGrowingFunction_IndexTwoInputTwo_ComputesCorrectly()
        {
            // Act
            BigInteger result = FastGrowingFunction(2, 2);

            // Assert
            Assert.Equal(8, result);
        }

        [Fact]
        public void FastGrowingFunction_IndexThreeInputOne_ComputesCorrectly()
        {
            // Act
            BigInteger result = FastGrowingFunction(3, 1);

            // Assert
            Assert.Equal(2, result);
        }
    }
}
