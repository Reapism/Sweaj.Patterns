using Geo = Sweaj.Patterns.Math.MathExt.Geometry;

namespace Sweaj.Patterns.Tests.Math.Geometry
{
    public class AreaOfRectangleTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(5, 0, 0)]
        [InlineData(0, 5, 0)]
        [InlineData(3, 4, 12)]
        [InlineData(10.5, 2, 21)]
        public void AreaOfRectangle_ValidInputs_ReturnsExpected(double length, double width, double expected)
        {
            // Act
            double area = Geo.AreaOfRectangle(length, width);

            // Assert
            Assert.Equal(expected, area);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(5, -1)]
        [InlineData(-3, -4)]
        public void AreaOfRectangle_NegativeInputs_ThrowsArgumentOutOfRangeException(double length, double width)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Geo.AreaOfRectangle(length, width));
        }
    }
}
