using Geo = Sweaj.Patterns.Math.MathExt.Geometry;

namespace Sweaj.Patterns.Tests.Math.Geometry;

public class PerimeterOfRectangleTests
{
    [Theory]
    [InlineData(3, 4, 14)]
    [InlineData(0, 5, 10)]
    public void PerimeterOfRectangle_ValidInputs_ReturnsExpected(double length, double width, double expected)
    {
        Assert.Equal(expected, Geo.PerimeterOfRectangle(length, width));
    }

    [Theory]
    [InlineData(-1, 2)]
    [InlineData(3, -2)]
    public void PerimeterOfRectangle_NegativeInputs_Throws(double length, double width)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Geo.PerimeterOfRectangle(length, width));
    }
}
