using Stats = Sweaj.Patterns.Math.MathExt.Statistics;

namespace Sweaj.Patterns.Tests.Math.Statistics;

public class StatisticalFunctionsTests
{
    public static IEnumerable<object[]> MeanTestData => new List<object[]>
    {
        new object[] { new List<int> { 1, 2, 3, 4, 5 }, 3.0 },
        new object[] { new List<double> { 2.5, 2.5 }, 2.5 },
        new object[] { new List<int> { -2, 0, 2 }, 0.0 },
        new object[] { new List<int> { 100 }, 100.0 }
    };

    [Theory]
    [MemberData(nameof(MeanTestData))]
    public void Mean_ValidInput_ReturnsExpected<T>(IEnumerable<T> data, double expected) where T : struct, IConvertible
    {
        var result = Stats.Mean(data);
        Assert.Equal(expected, result, 5);
    }

    [Fact]
    public void Mean_EmptyInput_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => Stats.Mean(new List<int>()));
    }

    [Fact]
    public void Mean_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Stats.Mean<int>(null));
    }

    public static IEnumerable<object[]> MedianTestData => new List<object[]>
    {
        new object[] { new List<int> { 1, 3, 2 }, 2.0 },
        new object[] { new List<int> { 1, 2, 3, 4 }, 2.5 },
        new object[] { new List<double> { 1.0, 1.0, 1.0 }, 1.0 },
        new object[] { new List<int> { 0, 10 }, 5.0 }
    };

    [Theory]
    [MemberData(nameof(MedianTestData))]
    public void Median_ValidInput_ReturnsExpected<T>(IEnumerable<T> data, double expected) where T : struct, IConvertible
    {
        var result = Stats.Median(data);
        Assert.Equal(expected, result, 5);
    }

    [Fact]
    public void Median_EmptyInput_ReturnsZero()
    {
        Assert.Equal(0.0, Stats.Median(new List<int>()));
    }

    [Fact]
    public void Median_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Stats.Median<int>(null));
    }

    public static IEnumerable<object[]> ModeTestData => new List<object[]>
    {
        new object[] { new List<int> { 1, 2, 2, 3 }, 2.0 },
        new object[] { new List<int> { 5, 5, 5, 5 }, 5.0 },
        new object[] { new List<double> { 1.0, 2.0, 1.0 }, 1.0 }
    };

    [Theory]
    [MemberData(nameof(ModeTestData))]
    public void Mode_ValidInput_ReturnsExpected<T>(IEnumerable<T> data, double expected) where T : struct, IConvertible
    {
        var result = Stats.Mode(data);
        Assert.Equal(expected, result, 5);
    }

    [Fact]
    public void Mode_EmptyInput_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => Stats.Mode(new List<int>()));
    }

    [Fact]
    public void Mode_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Stats.Mode<int>(null));
    }

    public static IEnumerable<object[]> VarianceTestData => new List<object[]>
    {
        new object[] { new List<int> { 1, 2, 3 }, 0.666666 },
        new object[] { new List<int> { 2, 2, 2 }, 0.0 },
        new object[] { new List<int> { -1, 0, 1 }, 0.666666 }
    };

    [Theory]
    [MemberData(nameof(VarianceTestData))]
    public void Variance_ValidInput_ReturnsExpected<T>(IEnumerable<T> data, double expected) where T : struct, IConvertible
    {
        var result = Stats.Variance(data);
        Assert.Equal(expected, result, 5);
    }

    [Fact]
    public void Variance_EmptyInput_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => Stats.Variance(new List<int>()));
    }

    [Fact]
    public void Variance_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Stats.Variance<int>(null));
    }

    [Theory]
    [InlineData(new double[] { 1, 2, 3 }, 0.8164965)]
    [InlineData(new double[] { 4, 4, 4 }, 0.0)]
    public void StandardDeviation_ValidInput_ReturnsExpected(double[] input, double expected)
    {
        var result = Stats.StandardDeviation(input);
        Assert.Equal(expected, result, 5);
    }

    [Fact]
    public void StandardDeviation_EmptyInput_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => Stats.StandardDeviation(new List<int>()));
    }

    [Fact]
    public void StandardDeviation_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Stats.StandardDeviation<int>(null));
    }
}
