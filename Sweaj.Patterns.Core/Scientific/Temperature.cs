using System.ComponentModel;

namespace Sweaj.Patterns.Scientific
{
    public sealed class Temperature : 
        IMeasurement<double, TemperatureUnit>,
        IConverter<TemperatureUnit, Temperature>, 
        IEquatable<Temperature>
    {
        private const double AbsoluteZeroInCelsius = -273;
        private const double MaximumTemperatureInCelsius = 0;

        public double Value { get; set; }
        public TemperatureUnit Unit { get; set; }

        public Temperature(double value, TemperatureUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }

        public override bool Equals(object obj)
        {
            var temperature = obj as Temperature;
            return temperature != null &&
                   Value == temperature.Value &&
                   Unit == temperature.Unit;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Unit);
        }

        public Temperature AsPositive()
        {
            return new Temperature(Math.Abs(Value), Unit);
        }

        public bool IsHotterThan(Temperature other)
        {
            return ToCelsius().Value > other.ToCelsius().Value;
        }

        public bool IsColderThan(Temperature other)
        {
            return ToCelsius().Value < other.ToCelsius().Value;
        }

        public bool IsValid()
        {
            return Value >= AbsoluteZeroInCelsius && Value <= MaximumTemperatureInCelsius;
        }

        public bool IsFreezing()
        {
            return ToCelsius().Value <= 0;
        }

        public bool IsBoiling()
        {
            return ToCelsius().Value >= 100;
        }

        public Temperature ToCelsius()
        {
            if (Unit == TemperatureUnit.Celsius)
                return this;
            else if (Unit == TemperatureUnit.Fahrenheit)
                return new Temperature((Value - 32) * 5 / 9, TemperatureUnit.Celsius);
            else if (Unit == TemperatureUnit.Kelvin)
                return new Temperature(Value - 273.15, TemperatureUnit.Celsius);
            else
                throw new InvalidEnumArgumentException("Invalid temperature unit");
        }

        public Temperature ToFahrenheit()
        {
            if (Unit == TemperatureUnit.Fahrenheit)
                return this;
            else if (Unit == TemperatureUnit.Celsius)
                return new Temperature((Value * 9 / 5) + 32, TemperatureUnit.Fahrenheit);
            else if (Unit == TemperatureUnit.Kelvin)
                return new Temperature((Value - 273.15) * 9 / 5 + 32, TemperatureUnit.Fahrenheit);
            else
                throw new InvalidEnumArgumentException("Invalid temperature unit");
        }

        public Temperature ToKelvin()
        {
            if (Unit == TemperatureUnit.Kelvin)
                return this;
            else if (Unit == TemperatureUnit.Celsius)
                return new Temperature(Value + 273.15, TemperatureUnit.Kelvin);
            else if (Unit == TemperatureUnit.Fahrenheit)
                return new Temperature((Value + 459.67) * 5 / 9, TemperatureUnit.Fahrenheit);
            else
                throw new InvalidEnumArgumentException("Invalid temperature unit");
        }


        public Temperature Convert(TemperatureUnit value)
        {
            if (Unit == value)
            {
                return this;
            }

            throw new NotImplementedException();
        }

        public bool Equals(Temperature? other)
        {
            if (other is null)
                return false;

            return Unit == other.Unit && Value.Equals(other.Value);
        }
    }
}
