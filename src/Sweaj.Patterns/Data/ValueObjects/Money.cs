using Sweaj.Patterns.Data.Values;
using System.Globalization;

namespace Sweaj.Patterns.Data.ValueObjects
{
    /// <summary>
    /// Represents a monetary value with currency and precision.
    /// </summary>
    /// <remarks>
    /// This struct ensures value integrity, supports arithmetic and comparison operations,
    /// and validates currency consistency where applicable.
    /// </remarks>
    public readonly struct Money : IValueProvider<decimal>, IEquatable<Money>, IComparable<Money>, IComparer<Money>
    {
        private const string DefaultCurrency = "USD";
        private const int DefaultDecimalPlaces = 2;

        /// <summary>
        /// Gets the monetary amount.
        /// </summary>
        public decimal Value { get; }

        /// <summary>
        /// Gets the ISO currency code.
        /// </summary>
        public string Currency { get; }

        /// <summary>
        /// Gets the number of decimal places used for rounding and display.
        /// </summary>
        public int DecimalPlaces { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Money"/> struct.
        /// </summary>
        /// <param name="value">The monetary value.</param>
        /// <param name="currency">The ISO currency code.</param>
        /// <param name="decimalPlaces">The number of decimal places.</param>
        public Money(decimal value, string currency, int decimalPlaces)
        {
            Value = Guard.Against.Negative(value, exceptionCreator: () => throw new InvalidOperationException("The amount of money cannot be negative itself."));
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
            DecimalPlaces = decimalPlaces >= 0 ? decimalPlaces : throw new ArgumentOutOfRangeException(nameof(decimalPlaces));
        }

        #region Factory Methods

        /// <summary>
        /// Creates a <see cref="Money"/> instance from an <see cref="int"/>.
        /// </summary>
        public static Money FromInt(int value, string currency = DefaultCurrency, int decimalPlaces = DefaultDecimalPlaces) =>
            new((decimal)value, currency, decimalPlaces);

        /// <summary>
        /// Creates a <see cref="Money"/> instance from a <see cref="decimal"/>.
        /// </summary>
        public static Money FromDecimal(decimal value, string currency = DefaultCurrency, int decimalPlaces = DefaultDecimalPlaces) =>
            new(value, currency, decimalPlaces);

        /// <summary>
        /// Creates a <see cref="Money"/> instance from a <see cref="double"/>.
        /// </summary>
        public static Money FromDouble(double value, string currency = DefaultCurrency, int decimalPlaces = DefaultDecimalPlaces) =>
            new(Convert.ToDecimal(value), currency, decimalPlaces);

        /// <summary>
        /// Creates a <see cref="Money"/> instance from a <see cref="float"/>.
        /// </summary>
        public static Money FromFloat(float value, string currency = DefaultCurrency, int decimalPlaces = DefaultDecimalPlaces) =>
            new(Convert.ToDecimal(value), currency, decimalPlaces);

        #endregion

        #region Operators

        /// <summary>
        /// Adds two <see cref="Money"/> instances with the same currency.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if currencies differ.</exception>
        public static Money operator +(Money a, Money b) => a.Add(b);

        /// <summary>
        /// Subtracts one <see cref="Money"/> instance from another with the same currency.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if currencies differ.</exception>
        /// <exception cref="InvalidOperationException">Thrown if result is negative.</exception>
        public static Money operator -(Money a, Money b) => a.Subtract(b);

        /// <summary>
        /// Multiplies a <see cref="Money"/> instance by a scalar.
        /// </summary>
        public static Money operator *(Money a, decimal b) => new(a.Value * b, a.Currency, a.DecimalPlaces);

        /// <summary>
        /// Divides a <see cref="Money"/> instance by a scalar.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if divisor is zero.</exception>
        public static Money operator /(Money a, decimal b)
        {
            Guard.Against.Zero(b);
            return new Money(a.Value / b, a.Currency, a.DecimalPlaces);
        }

        /// <summary>
        /// Determines whether one <see cref="Money"/> is less than another.
        /// </summary>
        public static bool operator <(Money a, Money b) => a.CompareTo(b) < 0;

        /// <summary>
        /// Determines whether one <see cref="Money"/> is greater than another.
        /// </summary>
        public static bool operator >(Money a, Money b) => a.CompareTo(b) > 0;

        /// <summary>
        /// Determines whether one <see cref="Money"/> is less than or equal to another.
        /// </summary>
        public static bool operator <=(Money a, Money b) => a.CompareTo(b) <= 0;

        /// <summary>
        /// Determines whether one <see cref="Money"/> is greater than or equal to another.
        /// </summary>
        public static bool operator >=(Money a, Money b) => a.CompareTo(b) >= 0;

        /// <summary>
        /// Determines whether two <see cref="Money"/> instances are equal.
        /// </summary>
        public static bool operator ==(Money a, Money b) => a.Equals(b);

        /// <summary>
        /// Determines whether two <see cref="Money"/> instances are not equal.
        /// </summary>
        public static bool operator !=(Money a, Money b) => !a.Equals(b);

        /// <summary>
        /// Implicitly converts a <see cref="Money"/> instance to its <see cref="decimal"/> value.
        /// </summary>
        public static implicit operator decimal(Money money) => money.Value;

        /// <summary>
        /// Implicitly converts a <see cref="decimal"/> value to a <see cref="Money"/> instance with default currency and precision.
        /// </summary>
        public static implicit operator Money(decimal value) => new(value, DefaultCurrency, DefaultDecimalPlaces);

        #endregion

        #region Methods

        private static void ValidateCurrency(Money a, Money b)
        {
            if (!string.Equals(a.Currency, b.Currency, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Cannot operate on money with different currencies.");
            }
        }

        /// <summary>
        /// Adds another money value to the current instance.
        /// </summary>
        public Money Add(Money other)
        {
            ValidateCurrency(this, other);
            return new Money(Value + other.Value, Currency, DecimalPlaces);
        }

        /// <summary>
        /// Subtracts another money value from the current instance.
        /// </summary>
        public Money Subtract(Money other)
        {
            ValidateCurrency(this, other);
            var result = Value - other.Value;
            if (result < 0) throw new InvalidOperationException("Resulting money cannot be negative.");
            return new Money(result, Currency, DecimalPlaces);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Value, Currency.ToUpperInvariant(), DecimalPlaces);

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Money other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(Money other) => Value == other.Value &&
                                           string.Equals(Currency, other.Currency, StringComparison.OrdinalIgnoreCase) &&
                                           DecimalPlaces == other.DecimalPlaces;

        /// <inheritdoc/>
        public int CompareTo(Money other)
        {
            ValidateCurrency(this, other);
            return Value.CompareTo(other.Value);
        }

        /// <inheritdoc/>
        public int Compare(Money x, Money y)
        {
            ValidateCurrency(x, y);
            return x.Value.CompareTo(y.Value);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var rounded = System.Math.Round(Value, DecimalPlaces);
            var format = "N" + DecimalPlaces;
            return string.Format(CultureInfo.InvariantCulture, "{0} {1}", rounded.ToString(format), Currency);
        }

        #endregion
    }
}
