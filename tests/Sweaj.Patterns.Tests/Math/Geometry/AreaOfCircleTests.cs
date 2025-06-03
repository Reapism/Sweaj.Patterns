using Geo = Sweaj.Patterns.Math.MathExt.Geometry;

namespace Sweaj.Patterns.Tests.Math.Geometry;

public class AreaOfCircleTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, System.Math.PI)]
    [InlineData(2, 4 * System.Math.PI)]
    public void AreaOfCircle_ValidInputs_ReturnsExpected(double radius, double expected)
    {
        Assert.Equal(expected, Geo.AreaOfCircle(radius), 5);
    }

    [Theory]
    [InlineData(-1)]
    public void AreaOfCircle_NegativeRadius_Throws(double radius)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Geo.AreaOfCircle(radius));
    }
}
