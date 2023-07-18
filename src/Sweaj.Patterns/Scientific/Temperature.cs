using Sweaj.Patterns.Converters;

namespace Sweaj.Patterns.Scientific
{
    public sealed class Temperature :
        IMeasurement<double, TemperatureUnit>,
        IConverter<TemperatureUnit, Temperature>,
        IEquatable<Temperature>
    {
        private const double AbsoluteZeroInCelsius = -273;
        private const double MaximumTemperatureInCelsius = 4_000_000_000_000_000_000_000_000_000_000_000_000_000_000_000_000_000_000_000D;
        private const double CelsiusToFahrenheitConversion = 9.0 / 5.0;
        private const double CelsiusToFahrenheitOffset = 32;
        private const double CelsiusToKelvin = 273.15;
        private const double MinimumTemperatureInFahrenheit = -459.67;

        public double Value { get; set; }
        public TemperatureUnit Unit { get; set; }

        private Temperature(double value, TemperatureUnit unit)
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
        public bool Equals(Temperature? other)
        {
            if (other is null)
                return false;

            return Unit == other.Unit && Value.Equals(other.Value);
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
            if (Unit == TemperatureUnit.Celsius)
                return Value >= AbsoluteZeroInCelsius && Value <= MaximumTemperatureInCelsius;

            var celsius = ToCelsius().Value;
            return celsius >= AbsoluteZeroInCelsius && celsius <= MaximumTemperatureInCelsius;
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
            switch (Unit)
            {
                case TemperatureUnit.Celsius:
                    return this;
                case TemperatureUnit.Fahrenheit:
                    return new Temperature((Value - CelsiusToFahrenheitOffset) / CelsiusToFahrenheitConversion + CelsiusToKelvin, TemperatureUnit.Celsius);
                case TemperatureUnit.Kelvin:
                    return new Temperature(Value - CelsiusToKelvin, TemperatureUnit.Celsius);
                default:
                    throw new ArgumentException("Invalid temperature unit");
            }
        }

        public Temperature ToFahrenheit()
        {
            switch (Unit)
            {
                case TemperatureUnit.Fahrenheit:
                    return this;
                case TemperatureUnit.Celsius:
                    return new Temperature((Value - CelsiusToKelvin) * CelsiusToFahrenheitConversion + CelsiusToFahrenheitOffset, TemperatureUnit.Fahrenheit);
                case TemperatureUnit.Kelvin:
                    return new Temperature(Value * CelsiusToFahrenheitConversion - (CelsiusToFahrenheitOffset + CelsiusToKelvin), TemperatureUnit.Fahrenheit);
                default:
                    throw new ArgumentException("Invalid temperature unit");
            }
        }

        public Temperature ToKelvin()
        {
            switch (Unit)
            {
                case TemperatureUnit.Kelvin:
                    return this;
                case TemperatureUnit.Celsius:
                    return new Temperature(Value + CelsiusToKelvin, TemperatureUnit.Kelvin);
                case TemperatureUnit.Fahrenheit:
                    return new Temperature((Value - CelsiusToFahrenheitOffset) / CelsiusToFahrenheitConversion + CelsiusToKelvin, TemperatureUnit.Kelvin);
                default:
                    throw new ArgumentException("Invalid temperature unit");
            }
        }

        public static Temperature FromCelcius(double value)
        {
            return new Temperature(value, TemperatureUnit.Celsius);
        }

        public static Temperature FromFahrenheit(double value)
        {
            return new Temperature(value, TemperatureUnit.Fahrenheit);
        }

        public static Temperature FromKelvin(double value)
        {
            return new Temperature(Guard.Against.AgainstExpression((v) => v < 0, value, "Kelvin cannot be less than zero."), TemperatureUnit.Kelvin);
        }

        public static Temperature From(double value, TemperatureUnit temperatureUnit)
        {
            return new Temperature(value, temperatureUnit);
        }

        public Temperature Convert(TemperatureUnit unit)
        {
            if (Unit == unit) return this;

            double newValue;
            switch (unit)
            {
                case TemperatureUnit.Celsius:
                    newValue = ToCelsius().Value;
                    break;
                case TemperatureUnit.Fahrenheit:
                    newValue = ToFahrenheit().Value;
                    break;
                case TemperatureUnit.Kelvin:
                    newValue = ToKelvin().Value;
                    break;
                default:
                    throw new ArgumentException("Invalid temperature unit");
            }

            return new Temperature(newValue, unit);
        }
    }
}
