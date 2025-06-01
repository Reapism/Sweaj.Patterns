using Sweaj.Patterns.Scientific;

namespace Sweaj.Patterns.Tests.Scientific;

public class TemperatureTests
{
    [Theory]
    [InlineData(0, TemperatureUnit.Celsius, 32)]
    [InlineData(100, TemperatureUnit.Celsius, 212)]
    [InlineData(32, TemperatureUnit.Fahrenheit, 32)]
    [InlineData(273.15, TemperatureUnit.Kelvin, 32)]
    public void ToFahrenheit_ReturnsCorrectValue(double value, TemperatureUnit unit, double expectedFahrenheit)
    {
        var temp = Temperature.From(value, unit);
        var result = temp.ToFahrenheit();

        Assert.Equal(expectedFahrenheit, result.Value, 2);
        Assert.Equal(TemperatureUnit.Fahrenheit, result.Unit);
    }

    [Theory]
    [InlineData(32, TemperatureUnit.Fahrenheit, 0)]
    [InlineData(100, TemperatureUnit.Celsius, 100)]
    [InlineData(373.15, TemperatureUnit.Kelvin, 100)]
    public void ToCelsius_ReturnsCorrectValue(double value, TemperatureUnit unit, double expectedCelsius)
    {
        var temp = Temperature.From(value, unit);
        var result = temp.ToCelsius();

        Assert.Equal(expectedCelsius, result.Value, 2);
        Assert.Equal(TemperatureUnit.Celsius, result.Unit);
    }

    [Theory]
    [InlineData(0, TemperatureUnit.Celsius, 273.15)]
    [InlineData(32, TemperatureUnit.Fahrenheit, 273.15)]
    [InlineData(273.15, TemperatureUnit.Kelvin, 273.15)]
    public void ToKelvin_ReturnsCorrectValue(double value, TemperatureUnit unit, double expectedKelvin)
    {
        var temp = Temperature.From(value, unit);
        var result = temp.ToKelvin();

        Assert.Equal(expectedKelvin, result.Value, 2);
        Assert.Equal(TemperatureUnit.Kelvin, result.Unit);
    }

    [Theory]
    [InlineData(-274, false)]
    [InlineData(-273.15, true)]
    [InlineData(4e48, false)]
    [InlineData(4e47, true)]
    public void IsValid_ReturnsCorrectValidation(double value, bool expectedIsValid)
    {
        var temp = Temperature.FromCelsius(value);
        Assert.Equal(expectedIsValid, temp.IsValid());
    }

    [Theory]
    [InlineData(-10, true)]
    [InlineData(0, true)]
    [InlineData(1, false)]
    public void IsFreezing_CorrectlyIdentifiesFreezingPoint(double celsius, bool expected)
    {
        var temp = Temperature.FromCelsius(celsius);
        Assert.Equal(expected, temp.IsFreezing());
    }

    [Theory]
    [InlineData(Temperature.AbsoluteZeroInCelsius, false)]
    [InlineData(double.MinValue, false)]
    [InlineData(-1, false)]
    [InlineData(99, false)]
    [InlineData(100, true)]
    [InlineData(101, true)]
    [InlineData(double.MaxValue, true)]
    [InlineData(Temperature.MaximumTemperatureInCelsius, true)]
    public void IsBoiling_CorrectlyIdentifiesBoilingPoint(double celsius, bool expected)
    {
        var temp = Temperature.FromCelsius(celsius);
        Assert.Equal(expected, temp.IsBoiling());
    }

    [Fact]
    public void AsPositive_ReturnsAbsoluteValue()
    {
        var temp = Temperature.FromCelsius(-20);
        var result = temp.AsPositive();
        Assert.Equal(20, result.Value);
    }

    [Fact]
    public void IsHotterThan_ReturnsCorrectComparison()
    {
        var t1 = Temperature.FromCelsius(30);
        var t2 = Temperature.FromCelsius(20);
        Assert.True(t1.IsHotterThan(t2));
    }

    [Fact]
    public void IsColderThan_ReturnsCorrectComparison()
    {
        var t1 = Temperature.FromCelsius(10);
        var t2 = Temperature.FromCelsius(20);
        Assert.True(t1.IsColderThan(t2));
    }

    [Fact]
    public void Convert_ReturnsCorrectConversionResult()
    {
        var temp = Temperature.FromCelsius(0);
        var result = temp.Convert(TemperatureUnit.Kelvin);

        Assert.True(result.IsSuccessful);
        Assert.Equal(273.15, result.Value.Value, 2);
        Assert.Equal(TemperatureUnit.Kelvin, result.Value.Unit);
    }

    [Fact]
    public void Convert_ReturnsFailureOnInvalidUnit()
    {
        var temp = Temperature.FromCelsius(0);
        var result = temp.Convert((TemperatureUnit)99);

        Assert.False(result.IsSuccessful);
        Assert.Contains("not supported", result.ErrorMessage);
    }
}
