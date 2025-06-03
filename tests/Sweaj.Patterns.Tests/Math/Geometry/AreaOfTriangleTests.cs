using Geo = Sweaj.Patterns.Math.MathExt.Geometry;

namespace Sweaj.Patterns.Tests.Math.Geometry;

public class AreaOfTriangleTests
{
    [Theory]
    [InlineData(0, 5, 0)]
    [InlineData(3, 4, 6)]
    public void AreaOfTriangle_ValidInputs_ReturnsExpected(double baseLength, double height, double expected)
    {
        Assert.Equal(expected, Geo.AreaOfTriangle(baseLength, height));
    }

    [Theory]
    [InlineData(-3, 4)]
    [InlineData(3, -4)]
    public void AreaOfTriangle_NegativeInputs_Throws(double baseLength, double height)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Geo.AreaOfTriangle(baseLength, height));
    }
}