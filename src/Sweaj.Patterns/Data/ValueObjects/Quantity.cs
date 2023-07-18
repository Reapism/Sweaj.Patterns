namespace Sweaj.Patterns.Data.ValueObjects
{
    public sealed class Quantity
    {
        private readonly decimal _value;
        private readonly string _unit;

        /// <summary>
        /// Initializes a new instance of the <see cref="Quantity"/> class.
        /// </summary>
        /// <param name="value">The value of the quantity.</param>
        /// <param name="unit">The unit of the quantity.</param>
        private Quantity(decimal value, string unit)
        {
            _value = value;
            _unit = unit;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Quantity"/> class.
        /// </summary>
        /// <param name="value">The value of the quantity.</param>
        /// <param name="unit">The unit of the quantity.</param>
        /// <returns>A new instance of the <see cref="Quantity"/> class.</returns>
        public static Quantity Create(decimal value, string unit)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(unit))
            {
                throw new ArgumentException("Unit cannot be null or empty.", nameof(unit));
            }

            return new Quantity(value, unit);
        }

        /// <summary>
        /// Gets the value of the quantity.
        /// </summary>
        public decimal Value => _value;

        /// <summary>
        /// Gets the unit of the quantity.
        /// </summary>
        public string Unit => _unit;

        public override string ToString()
        {
            return $"{_value} {_unit}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as Quantity;
            if (other == null)
            {
                return false;
            }

            return _value == other._value && _unit == other._unit;
        }

        public override int GetHashCode()
        {
            var hashCode = -1405383098;
            hashCode = hashCode * -1521134295 + _value.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_unit);
            return hashCode;
        }
    }
}
