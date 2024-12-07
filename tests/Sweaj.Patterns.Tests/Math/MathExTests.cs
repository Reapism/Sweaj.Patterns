using Sweaj.Patterns.Math;

namespace Sweaj.Patterns.Tests.Math
{
    public class MathExTests
    {
        [Theory]
        [InlineData(105, 100, 5)]          // Positive error: 5% difference
        [InlineData(95, 100, 5)]           // Negative error: 5% difference
        [InlineData(150, 100, 50)]         // Larger positive error: 50% difference
        [InlineData(50, 100, 50)]          // Larger negative error: 50% difference
        [InlineData(100, 100, 0)]          // No error: 0% difference
        [InlineData(-95, -100, 5)]         // Negative values with small error: 5% difference
        [InlineData(-105, -100, 5)]        // Negative values with small error: 5% difference
        [InlineData(-150, -100, 50)]       // Negative values with large error: 50% difference
        [InlineData(0, 100, 100)]          // Experimental value is zero: 100% error
        [InlineData(100, 0, double.PositiveInfinity)] // Theoretical value is zero: division by zero case
        [InlineData(-100, 100, 200)]       // Opposite signs: 200% difference
        [InlineData(100, -100, 200)]       // Opposite signs: 200% difference
        [InlineData(0, 0, double.NaN)]              // Both values are zero: 0% error (special case)
        public void PercentError_ShouldReturnCorrectValue(double experimental, double theoretical, double expectedPercentError)
        {
            // Act
            var result = MathEx.PercentError(experimental, theoretical);

            // Assert
            Assert.Equal(expectedPercentError, result, 5); // Allow slight floating-point precision error
        }

        [Theory]
        [InlineData(100, 150, 50)]           // 50% increase
        [InlineData(100, 200, 100)]          // 100% increase
        [InlineData(100, 100, 0)]            // No increase
        [InlineData(100, 50, -50)]           // 50% decrease
        [InlineData(-100, -50, 50)]          // Increase from negative: 50% increase
        [InlineData(-100, -200, -100)]       // Decrease from negative: -100% decrease
        [InlineData(-100, 100, 200)]         // Crossing zero: 200% increase
        [InlineData(100, -100, -200)]        // Crossing zero: -200% decrease
        [InlineData(0, 100, double.PositiveInfinity)]  // Division by zero: startValue is zero
        [InlineData(0, -100, double.NegativeInfinity)] // Division by zero: startValue is zero, negative final value
        [InlineData(-0.001, 0.001, 200)]     // Small negative to small positive: 200% increase
        [InlineData(0.001, -0.001, -200)]    // Small positive to small negative: -200% decrease
        public void PercentIncrease_ShouldReturnCorrectValue(double startValue, double finalValue, double expectedPercent)
        {
            // Act
            var result = MathEx.PercentIncrease(startValue, finalValue);

            // Assert
            Assert.Equal(expectedPercent, result, 5); // Allow slight floating-point precision error
        }
    }
}
