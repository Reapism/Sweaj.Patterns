using Geo = Sweaj.Patterns.Math.MathExt.Geometry;

namespace Sweaj.Patterns.Tests.Math.Geometry;

public class CircumferenceOfCircleTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 2 * System.Math.PI)]
    [InlineData(2, 4 * System.Math.PI)]
    public void CircumferenceOfCircle_ValidInputs_ReturnsExpected(double radius, double expected)
    {
        Assert.Equal(expected, Geo.CircumferenceOfCircle(radius), 5);
    }

    [Theory]
    [InlineData(-1)]
    public void CircumferenceOfCircle_NegativeRadius_Throws(double radius)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Geo.CircumferenceOfCircle(radius));
    }
}
