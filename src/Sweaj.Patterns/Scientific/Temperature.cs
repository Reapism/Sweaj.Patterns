using Sweaj.Patterns.Converters;

/// <summary>
/// Contains types related to scientific measurements, including temperature.
/// </summary>
namespace Sweaj.Patterns.Scientific
{
    /// <summary>
    /// Represents the available units for temperature measurement.
    /// </summary>
    public enum TemperatureUnit : byte
    {
        /// <summary>
        /// Celsius temperature unit (°C).
        /// </summary>
        Celsius,

        /// <summary>
        /// Fahrenheit temperature unit (°F).
        /// </summary>
        Fahrenheit,

        /// <summary>
        /// Kelvin temperature unit (K).
        /// </summary>
        Kelvin
    }

    /// <summary>
    /// Represents a temperature measurement and provides conversion, comparison, and validation functionality.
    /// </summary>
    public sealed class Temperature :
        IMeasurement<double, TemperatureUnit>,
        IConverter<TemperatureUnit, Temperature>,
        IEquatable<Temperature>
    {
        public const double AbsoluteZeroInCelsius = -273.15;
        public const double MaximumTemperatureInCelsius = 4e47;
        private const double CelsiusToFahrenheitConversion = 9.0 / 5.0;
        private const double CelsiusToFahrenheitOffset = 32;
        private const double CelsiusToKelvin = 273.15;

        /// <summary>
        /// Gets or sets the numeric value of the temperature.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the unit of the temperature.
        /// </summary>
        public TemperatureUnit Unit { get; set; }

        private Temperature(double value, TemperatureUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <inheritdoc/>
        public override string ToString() => $"{Value} {Unit}";

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Temperature other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(Temperature? other)
        {
            if (other is null) return false;
            return Unit == other.Unit && Value.Equals(other.Value);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Value, Unit);

        /// <summary>
        /// Returns a new instance of <see cref="Temperature"/> with an absolute value.
        /// </summary>
        /// <returns>A temperature with a non-negative magnitude.</returns>
        public Temperature AsPositive() => new Temperature(System.Math.Abs(Value), Unit);

        /// <summary>
        /// Determines if this temperature is hotter than another.
        /// </summary>
        /// <param name="other">Temperature to compare.</param>
        /// <returns><c>true</c> if this is hotter; otherwise, <c>false</c>.</returns>
        public bool IsHotterThan(Temperature other) => ToCelsius().Value > other.ToCelsius().Value;

        /// <summary>
        /// Determines if this temperature is colder than another.
        /// </summary>
        /// <param name="other">Temperature to compare.</param>
        /// <returns><c>true</c> if this is colder; otherwise, <c>false</c>.</returns>
        public bool IsColderThan(Temperature other) => ToCelsius().Value < other.ToCelsius().Value;

        /// <summary>
        /// Checks if the temperature is within valid physical bounds.
        /// </summary>
        /// <returns><c>true</c> if valid; otherwise, <c>false</c>.</returns>
        public bool IsValid()
        {
            double celsius = ToCelsius().Value;
            return celsius >= AbsoluteZeroInCelsius && celsius <= MaximumTemperatureInCelsius;
        }

        /// <summary>
        /// Determines if the temperature is at or below freezing.
        /// </summary>
        /// <returns><c>true</c> if freezing or lower; otherwise, <c>false</c>.</returns>
        public bool IsFreezing() => ToCelsius().Value <= 0;

        /// <summary>
        /// Determines if the temperature is at or above boiling.
        /// </summary>
        /// <returns><c>true</c> if boiling or higher; otherwise, <c>false</c>.</returns>
        public bool IsBoiling() => ToCelsius().Value >= 100;

        /// <summary>
        /// Converts to Celsius.
        /// </summary>
        /// <returns>Temperature in Celsius.</returns>
        public Temperature ToCelsius()
        {
            return Unit switch
            {
                TemperatureUnit.Celsius => this,
                TemperatureUnit.Fahrenheit => new Temperature((Value - CelsiusToFahrenheitOffset) / CelsiusToFahrenheitConversion, TemperatureUnit.Celsius),
                TemperatureUnit.Kelvin => new Temperature(Value - CelsiusToKelvin, TemperatureUnit.Celsius),
                _ => throw new ArgumentException("Invalid temperature unit", nameof(Unit)),
            };
        }

        /// <summary>
        /// Converts to Fahrenheit.
        /// </summary>
        /// <returns>Temperature in Fahrenheit.</returns>
        public Temperature ToFahrenheit()
        {
            return Unit switch
            {
                TemperatureUnit.Fahrenheit => this,
                TemperatureUnit.Celsius => new Temperature(Value * CelsiusToFahrenheitConversion + CelsiusToFahrenheitOffset, TemperatureUnit.Fahrenheit),
                TemperatureUnit.Kelvin => new Temperature((Value - CelsiusToKelvin) * CelsiusToFahrenheitConversion + CelsiusToFahrenheitOffset, TemperatureUnit.Fahrenheit),
                _ => throw new ArgumentException("Invalid temperature unit", nameof(Unit)),
            };
        }

        /// <summary>
        /// Converts to Kelvin.
        /// </summary>
        /// <returns>Temperature in Kelvin.</returns>
        public Temperature ToKelvin()
        {
            return Unit switch
            {
                TemperatureUnit.Kelvin => this,
                TemperatureUnit.Celsius => new Temperature(Value + CelsiusToKelvin, TemperatureUnit.Kelvin),
                TemperatureUnit.Fahrenheit => new Temperature((Value - CelsiusToFahrenheitOffset) / CelsiusToFahrenheitConversion + CelsiusToKelvin, TemperatureUnit.Kelvin),
                _ => throw new ArgumentException("Invalid temperature unit", nameof(Unit)),
            };
        }

        /// <summary>
        /// Creates a temperature in Celsius.
        /// </summary>
        /// <param name="value">Temperature value.</param>
        /// <returns>Temperature in Celsius.</returns>
        public static Temperature FromCelsius(double value) => new Temperature(value, TemperatureUnit.Celsius);

        /// <summary>
        /// Creates a temperature in Fahrenheit.
        /// </summary>
        /// <param name="value">Temperature value.</param>
        /// <returns>Temperature in Fahrenheit.</returns>
        public static Temperature FromFahrenheit(double value) => new Temperature(value, TemperatureUnit.Fahrenheit);

        /// <summary>
        /// Creates a temperature in Kelvin with validation.
        /// </summary>
        /// <param name="value">Temperature value.</param>
        /// <returns>Temperature in Kelvin.</returns>
        /// <exception cref="ArgumentException">Thrown if value is negative.</exception>
        public static Temperature FromKelvin(double value)
        {
            return new Temperature(Guard.Against.Expression(v => v < 0, value, "Kelvin cannot be less than zero."), TemperatureUnit.Kelvin);
        }

        /// <summary>
        /// Creates a temperature in the specified unit.
        /// </summary>
        /// <param name="value">Temperature value.</param>
        /// <param name="temperatureUnit">Unit of temperature.</param>
        /// <returns>Temperature instance.</returns>
        public static Temperature From(double value, TemperatureUnit temperatureUnit)
        {
            return new Temperature(value, temperatureUnit);
        }

        /// <summary>
        /// Converts to the specified unit.
        /// </summary>
        /// <param name="value">Target unit.</param>
        /// <returns>A <see cref="ConversionResult{T}"/> representing the result.</returns>
        public ConversionResult<Temperature> Convert(TemperatureUnit value)
        {
            if (Unit == value)
                return ConversionResult<Temperature>.FromSuccessful(this);

            try
            {
                return value switch
                {
                    TemperatureUnit.Celsius => ConversionResult<Temperature>.FromSuccessful(ToCelsius()),
                    TemperatureUnit.Fahrenheit => ConversionResult<Temperature>.FromSuccessful(ToFahrenheit()),
                    TemperatureUnit.Kelvin => ConversionResult<Temperature>.FromSuccessful(ToKelvin()),
                    _ => ConversionResult<Temperature>.FromFailure("The provided unit is not supported")
                };
            }
            catch (Exception ex)
            {
                return ConversionResult<Temperature>.FromFailure($"Conversion failed: {ex.Message}");
            }
        }
    }
}
