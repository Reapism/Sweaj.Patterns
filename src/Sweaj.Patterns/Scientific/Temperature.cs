using Sweaj.Patterns.Converters;

namespace Sweaj.Patterns.Scientific
{
    /// <summary>
    /// Enum representing temperature units.
    /// </summary>
    public enum TemperatureUnit : byte
    {
        /// <summary>
        /// Celsius temperature unit.
        /// </summary>
        Celsius,

        /// <summary>
        /// Fahrenheit temperature unit.
        /// </summary>
        Fahrenheit,

        /// <summary>
        /// Kelvin temperature unit.
        /// </summary>
        Kelvin
    }

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
            return Unit switch
            {
                TemperatureUnit.Celsius => this,
                TemperatureUnit.Fahrenheit => new Temperature((Value - CelsiusToFahrenheitOffset) / CelsiusToFahrenheitConversion + CelsiusToKelvin, TemperatureUnit.Celsius),
                TemperatureUnit.Kelvin => new Temperature(Value - CelsiusToKelvin, TemperatureUnit.Celsius),
                _ => throw new ArgumentException("Invalid temperature unit"),
            };
        }

        public Temperature ToFahrenheit()
        {
            return Unit switch
            {
                TemperatureUnit.Fahrenheit => this,
                TemperatureUnit.Celsius => new Temperature((Value - CelsiusToKelvin) * CelsiusToFahrenheitConversion + CelsiusToFahrenheitOffset, TemperatureUnit.Fahrenheit),
                TemperatureUnit.Kelvin => new Temperature(Value * CelsiusToFahrenheitConversion - (CelsiusToFahrenheitOffset + CelsiusToKelvin), TemperatureUnit.Fahrenheit),
                _ => throw new ArgumentException("Invalid temperature unit"),
            };
        }

        public Temperature ToKelvin()
        {
            return Unit switch
            {
                TemperatureUnit.Kelvin => this,
                TemperatureUnit.Celsius => new Temperature(Value + CelsiusToKelvin, TemperatureUnit.Kelvin),
                TemperatureUnit.Fahrenheit => new Temperature((Value - CelsiusToFahrenheitOffset) / CelsiusToFahrenheitConversion + CelsiusToKelvin, TemperatureUnit.Kelvin),
                _ => throw new ArgumentException("Invalid temperature unit"),
            };
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

        public ConversionResult<Temperature> Convert(TemperatureUnit value)
        {
            if (Unit == value)
                return ConversionResult<Temperature>.FromSuccessful(this);

            double newValue;
            switch (value)
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
                    return ConversionResult<Temperature>.FromFailure("The provided unit is not supported");
            }

            return ConversionResult<Temperature>.FromSuccessful(From(newValue, value));
        }
    }
}
