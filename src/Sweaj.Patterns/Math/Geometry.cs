namespace Sweaj.Patterns.Math;

public static partial class MathExt
{
    public static partial class Geometry
    {
        /// <summary>
        /// Calculates the area of a rectangle.
        /// </summary>
        /// <param name="length">The non-negative length of the rectangle.</param>
        /// <param name="width">The non-negative width of the rectangle.</param>
        /// <returns>The area as a <see cref="double"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="length"/> or <paramref name="width"/> is negative.
        /// </exception>
        /// <remarks>
        /// Formula: <c>length × width</c>
        /// </remarks>
        public static double AreaOfRectangle(double length, double width)
        {
            if (length < 0 || width < 0)
                throw new ArgumentOutOfRangeException("Dimensions must be non-negative.");

            return length * width;
        }


        /// <summary>
        /// Calculates the perimeter of a rectangle.
        /// </summary>
        /// <param name="length">The non-negative length of the rectangle.</param>
        /// <param name="width">The non-negative width of the rectangle.</param>
        /// <returns>The perimeter as a <see cref="double"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="length"/> or <paramref name="width"/> is negative.
        /// </exception>
        /// <remarks>
        /// Formula: <c>2 × (length + width)</c>
        /// </remarks>
        public static double PerimeterOfRectangle(double length, double width)
        {
            if (length < 0 || width < 0)
                throw new ArgumentOutOfRangeException("Dimensions must be non-negative.");

            return 2 * (length + width);
        }

        /// <summary>
        /// Calculates the area of a circle.
        /// </summary>
        /// <param name="radius">The non-negative radius of the circle.</param>
        /// <returns>The area as a <see cref="double"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="radius"/> is negative.
        /// </exception>
        /// <remarks>
        /// Formula: <c>π × radius²</c>
        /// </remarks>
        public static double AreaOfCircle(double radius)
        {
            if (radius < 0)
                throw new ArgumentOutOfRangeException("Radius must be non-negative.");

            return System.Math.PI * radius * radius;
        }

        /// <summary>
        /// Calculates the circumference (perimeter) of a circle.
        /// </summary>
        /// <param name="radius">The non-negative radius of the circle.</param>
        /// <returns>The circumference as a <see cref="double"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="radius"/> is negative.
        /// </exception>
        /// <remarks>
        /// Formula: <c>2 × π × radius</c>
        /// </remarks>
        public static double CircumferenceOfCircle(double radius)
        {
            if (radius < 0)
                throw new ArgumentOutOfRangeException("Radius must be non-negative.");

            return 2 * System.Math.PI * radius;
        }

        /// <summary>
        /// Calculates the area of a triangle given its base and height.
        /// </summary>
        /// <param name="baseLength">The non-negative base of the triangle.</param>
        /// <param name="height">The non-negative height of the triangle.</param>
        /// <returns>The area as a <see cref="double"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="baseLength"/> or <paramref name="height"/> is negative.
        /// </exception>
        /// <remarks>
        /// Formula: <c>½ × base × height</c>
        /// </remarks>
        public static double AreaOfTriangle(double baseLength, double height)
        {
            if (baseLength < 0 || height < 0)
                throw new ArgumentOutOfRangeException("Base and height must be non-negative.");

            return 0.5 * baseLength * height;
        }
    }
}