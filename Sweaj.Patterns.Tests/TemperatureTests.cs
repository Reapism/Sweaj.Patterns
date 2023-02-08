using Sweaj.Patterns.Scientific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweaj.Patterns.Tests
{
    public class TemperatureTests
    {
        [Theory]
        [InlineData(TemperatureUnit.Celsius, 10D, 10D, 50D, 10D)]
        [InlineData(TemperatureUnit.Fahrenheit, 32D, 90D, 32D, 0D)]
        [InlineData(TemperatureUnit.Kelvin, 273.15D, 300D, 80.33D, 273.15)]
        public void TestTemperatureConversion(
            TemperatureUnit fromUnit, 
            double fromValue,
            double toCelsius, 
            double toFahrenheit,
            double toKelvin)
        {
            var temperature = Temperature.From(fromValue, fromUnit);

            var celsius = temperature.Convert(TemperatureUnit.Celsius);
            Assert.Equal(toCelsius, celsius.Value, 2);

            var fahrenheit = temperature.Convert(TemperatureUnit.Fahrenheit);
            Assert.Equal(toFahrenheit, fahrenheit.Value, 2);

            var kelvin = temperature.Convert(TemperatureUnit.Kelvin);
            Assert.Equal(toKelvin, kelvin.Value, 2);
        }
    }
}
