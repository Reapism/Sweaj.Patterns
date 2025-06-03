namespace Sweaj.Patterns.Data.ValueObjects
{
    public sealed class Quantity
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Quantity"/> class.
        /// </summary>
        /// <param name="value">The value of the quantity.</param>
        /// <param name="unit">The unit of the quantity.</param>
        private Quantity(decimal value, string unit)
        {
            Value = value;
            Unit = unit;
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
        public decimal Value { get; }

        /// <summary>
        /// Gets the unit of the quantity.
        /// </summary>
        public string Unit { get; }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as Quantity;
            if (other == null)
            {
                return false;
            }

            return Value == other.Value && Unit == other.Unit;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Unit);
        }
    }
}
