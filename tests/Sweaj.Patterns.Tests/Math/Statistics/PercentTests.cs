using Stats = Sweaj.Patterns.Math.MathExt.Statistics;

namespace Sweaj.Patterns.Tests.Math.Statistics;

public class PercentTests
{
    [Theory]
    [InlineData(100, 150, 50.0)]
    [InlineData(100, 50, -50.0)]
    [InlineData(-100, -50, 50.0)]
    [InlineData(-100, -150, -50.0)]
    public void PercentIncrease_ValidInputs_ReturnsExpected(double start, double final, double expected)
    {
        var result = Stats.PercentIncrease(start, final);
        Assert.Equal(expected, result, 5);
    }

    [Fact]
    public void PercentIncrease_ZeroStartValue_ThrowsDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => Stats.PercentIncrease(0, 100));
    }

    [Theory]
    [InlineData(105, 100, 5)]
    [InlineData(95, 100, 5)]
    [InlineData(150, 100, 50)]
    [InlineData(50, 100, 50)]
    [InlineData(100, 100, 0)]
    [InlineData(-95, -100, 5)]
    [InlineData(-105, -100, 5)]
    [InlineData(-150, -100, 50)]
    [InlineData(0, 100, 100)]
    [InlineData(-100, 100, 200)]
    [InlineData(100, -100, 200)]
    public void PercentError_ValidInputs_ReturnsExpected(double experimental, double theoretical, double expected)
    {
        if (theoretical == 0)
            Assert.Throws<DivideByZeroException>(() => Stats.PercentError(experimental, theoretical));
        else
            Assert.Equal(expected, Stats.PercentError(experimental, theoretical), 5);
    }

    [Theory]
    [InlineData(80, 100, 25.0)]
    [InlineData(100, 80, -20.0)]
    [InlineData(-100, -50, 50.0)]
    [InlineData(-100, -150, -50.0)]
    public void PercentChange_ValidInputs_ReturnsExpected(double original, double newVal, double expected)
    {
        var result = Stats.PercentChange(original, newVal);
        Assert.Equal(expected, result, 5);
    }

    [Fact]
    public void PercentChange_ZeroOriginalValue_ThrowsDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => Stats.PercentChange(0, 100));
    }
}
