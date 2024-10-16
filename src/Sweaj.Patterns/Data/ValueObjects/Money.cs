using Sweaj.Patterns.Data.Values;
using System.Globalization;

namespace Sweaj.Patterns.Data.ValueObjects
{
    public readonly struct Money : IValueProvider<decimal>, IEquatable<Money>, IComparable<Money>, IComparer<Money>
    {
        private const string DefaultCurrency = "USD";
        private const int DefaultDecimalPlaces = 2;
        public decimal Value { get; }
        public string Currency { get; }
        public int DecimalPlaces { get; }

        public Money(decimal value, string currency, int decimalPlaces)
        {
            Value = Guard.Against.Negative(value);
            Currency = currency;
            DecimalPlaces = decimalPlaces;
        }

        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new ArgumentException("Cannot add money of different currencies.");
            }
            return new Money(a.Value + b.Value, a.Currency, a.DecimalPlaces);
        }

        public static Money operator -(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new ArgumentException("Cannot subtract money of different currencies.");
            }
            if (a.Value < b.Value)
            {
                throw new ArgumentException("Resulting money value cannot be negative.");
            }
            return new Money(a.Value - b.Value, a.Currency, a.DecimalPlaces);
        }

        public static Money operator *(Money a, decimal b)
        {
            return new Money(a.Value * b, a.Currency, a.DecimalPlaces);
        }

        public static Money operator /(Money a, decimal b)
        {
            Guard.Against.Zero(b);
            return new Money(a.Value / b, a.Currency, a.DecimalPlaces);
        }

        public static bool operator <(Money a, Money b)
        {
            ValidateCurrency(a, b);
            return a.Value < b.Value;
        }

        public static bool operator >(Money a, Money b)
        {
            ValidateCurrency(a, b);
            return a.Value > b.Value;
        }

        public static bool operator <=(Money a, Money b)
        {
            ValidateCurrency(a, b);
            return a.Value <= b.Value;
        }

        public static bool operator >=(Money a, Money b)
        {
            ValidateCurrency(a, b);
            return a.Value >= b.Value;
        }

        public static bool operator ==(Money a, Money b)
        {
            ValidateCurrency(a, b);

            return a.Value == b.Value;
        }
        public static bool operator !=(Money a, Money b)
        {
            ValidateCurrency(a, b);
            return a.Value != b.Value;
        }

        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }

        public static implicit operator Money(decimal value)
        {
            return new Money(value, DefaultCurrency, DefaultDecimalPlaces);
        }

        private static void ValidateCurrency(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new ArgumentException("Cannot compare money of different currencies.");
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Currency, DecimalPlaces);
        }

        public override string ToString()
        {
            var roundedValue = System.Math.Round(Value, DecimalPlaces);
            var format = "C" + DecimalPlaces;
            var culture = new CultureInfo(Currency);
            return roundedValue.ToString(format, culture);
        }

        public bool Equals(Money other)
        {
            return Value == other.Value
                && Currency == other.Currency
                && DecimalPlaces == other.DecimalPlaces;
        }

        public int Compare(Money x, Money y)
        {
            if (x.Value > y.Value) return 1;
            if (x.Value < y.Value) return -1;
            return 0;
        }

        public int CompareTo(Money other)
        {
            if (Value > other.Value) return 1;
            if (Value < other.Value) return -1;
            return 0;
        }

        public override bool Equals(object obj)
        {
            return obj is Money && Equals((Money)obj);
        }
    }
}
