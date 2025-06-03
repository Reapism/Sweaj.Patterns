using Ardalis.GuardClauses;
using System.Numerics;

namespace Sweaj.Patterns.Math;

/// <summary>
/// A set of extended mathematical formulas for common analytical computations such as percentage calculations, growth ratios, and comparative differences.
/// </summary>
/// <remarks>
/// This static class provides utilities typically needed in data analysis, financial calculations, and error estimation.
/// </remarks>
public static class MathExt
{
    public static class NumberTheory
    {

    }
    /// <summary>
    /// Provides algebraic utility methods.
    /// </summary>
    public static class Algebra
    {
        /// <summary>
        /// Calculates the factorial of a non-negative integer.
        /// </summary>
        /// <param name="n">The non-negative integer.</param>
        /// <returns>The factorial of <paramref name="n"/> as a <see cref="BigInteger"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="n"/> is negative.
        /// </exception>
        /// <remarks>
        /// This method uses recursion and returns the result as a <see cref="BigInteger"/> to prevent overflow.
        /// </remarks>
        /// <example>
        /// <code>
        /// BigInteger result = Algebra.Factorial(5); // result is 120
        /// </code>
        /// </example>
        public static BigInteger Factorial(int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), "Input must be non-negative.");

            return n <= 1 ? BigInteger.One : n * Factorial(n - 1);
        }

        /// <summary>
        /// Calculates the factorial of a non-negative <see cref="BigInteger"/> value.
        /// </summary>
        /// <param name="n">The non-negative <see cref="BigInteger"/>.</param>
        /// <returns>The factorial of <paramref name="n"/> as a <see cref="BigInteger"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="n"/> is negative.
        /// </exception>
        /// <remarks>
        /// This method uses recursion and supports arbitrarily large values of <paramref name="n"/>.
        /// </remarks>
        /// <example>
        /// <code>
        /// BigInteger result = Algebra.FactorialBig(BigInteger.Parse("30"));
        /// // result is 265252859812191058636308480000000
        /// </code>
        /// </example>
        public static BigInteger FactorialBig(BigInteger n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), "Input must be non-negative.");

            return n <= 1 ? BigInteger.One : n * FactorialBig(n - 1);
        }

        /// <summary>
        /// Evaluates the Fast Growing Hierarchy function F_index(input).
        /// </summary>
        /// <param name="index">The level of the hierarchy.</param>
        /// <param name="input">The input value.</param>
        /// <returns>The result of the FGH function as a <see cref="BigInteger"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="index"/> or <paramref name="input"/> is negative.
        /// </exception>
        /// <remarks>
        /// This method defines the fast-growing hierarchy using the recurrence:
        /// 
        /// f₀(n) = n + 1
        /// f_{α+1}(n) = f_α^n(n) (i.e., applying f_α to n, n times)
        /// f_α(n) = f_{α[n]}(n) if α is a limit ordinal
        /// 
        /// This implementation assumes a finite ordinal index and models the hierarchy recursively:
        /// F_0(input) = input + 1
        /// F_{index+1}(input) = F_index^input(input)
        /// 
        /// Be cautious with large values, as growth is extremely rapid.
        /// </remarks>
        /// <example>
        /// <code>
        /// BigInteger result = Algebra.FastGrowingFunction(1, 3); // result is 6
        /// </code>
        /// </example>
        public static BigInteger FastGrowingFunction(int index, BigInteger input)
        {
            if (index < 0 || input < 0)
                throw new ArgumentOutOfRangeException("Both index and input must be non-negative.");

            if (index == 0)
                return input + 1;

            BigInteger result = input;
            for (BigInteger i = 0; i < input; i++)
            {
                result = FastGrowingFunction(index - 1, result);
            }

            return result;
        }
    }
    /// <summary>
    /// Provides geometric calculations such as area and perimeter for basic shapes.
    /// </summary>
    public static class Geometry
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

    /// <summary>
    /// A set of extended mathematical formulas for common analytical computations such as percentage calculations, growth ratios, and comparative differences.
    /// </summary>
    public static class Statistics
    {
        /// <summary>
        /// Finds the most frequent character in a string.
        /// </summary>
        /// <param name="input">The input string to analyze.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="input"/> is null.</exception
        /// <exception cref="ArgumentException">Thrown if <paramref name="input"/> is empty.</exception>
        /// <returns>The character that occurs most frequently. If multiple, returns the first encountered.</returns>
        public static char MostFrequentCharacter([NotNull, ValidatedNotNull] string input)
        {
            Guard.Against.NullOrEmpty(input);
            return input.GroupBy(c => c)
                        .OrderByDescending(g => g.Count())
                        .ThenBy(g => g.Key)
                        .FirstOrDefault().Key;
        }

        /// <summary>
        /// Finds the least frequent character in a string.
        /// </summary>
        /// <param name="input">The input string to analyze.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="input"/> is null.</exception
        /// <exception cref="ArgumentException">Thrown if <paramref name="input"/> is empty.</exception>
        /// <returns>The character that occurs least frequently. If multiple, returns the first encountered.</returns>
        public static char LeastFrequentCharacter([NotNull, ValidatedNotNull] string input)
        {
            Guard.Against.NullOrEmpty(input);
            return input.GroupBy(c => c)
                        .OrderBy(g => g.Count())
                        .ThenBy(g => g.Key)
                        .FirstOrDefault().Key;
        }

        /// <summary>
        /// Calculates the mean (average) of a sequence of numbers.
        /// <para>Definition: Sum of all values divided by count of values.</para>
        /// <para>Formula: <c>mean = (Σxᵢ) / n</c></para>
        /// <para>Note: All values are converted to <c>double</c> using <c>Convert.ToDouble</c> for calculation.</para>
        /// </summary>
        public static double Mean<T>(IEnumerable<T> data) where T : struct, IConvertible
        {
            var doubleData = data.Select(x => Convert.ToDouble(x));
            return doubleData.Average();
        }

        /// <summary>
        /// Calculates the median of a sequence of numbers.
        /// <para>Definition: The middle value in an ordered list. If the list has an even number of elements, the median is the average of the two middle values.</para>
        /// <para>Formula: <c>median = middle value or (xₙ/₂ + xₙ/₂₊₁) / 2</c></para>
        /// <para>Note: All values are converted to <c>double</c> using <c>Convert.ToDouble</c> for calculation.</para>
        /// </summary>
        public static double Median<T>(IEnumerable<T> data) where T : struct, IConvertible
        {
            var ordered = data.Select(x => Convert.ToDouble(x)).OrderBy(x => x).ToList();
            int count = ordered.Count;
            if (count == 0) return 0;
            return count % 2 == 1 ? ordered[count / 2] : (ordered[(count / 2) - 1] + ordered[count / 2]) / 2.0;
        }

        /// <summary>
        /// Calculates the mode (most frequent value) of a sequence of numbers.
        /// <para>Definition: The value that appears most frequently in the data set.</para>
        /// <para>Formula: <c>mode = argmaxₓ(count(x))</c></para>
        /// <para>Note: All values are converted to <c>double</c> using <c>Convert.ToDouble</c> for calculation.</para>
        /// </summary>
        public static double Mode<T>(IEnumerable<T> data) where T : struct, IConvertible
        {
            return data.Select(x => Convert.ToDouble(x))
                       .GroupBy(x => x)
                       .OrderByDescending(g => g.Count())
                       .ThenBy(g => g.Key)
                       .First().Key;
        }

        /// <summary>
        /// Calculates the variance of a sequence of numbers.
        /// <para>Definition: The average of the squared differences from the mean.</para>
        /// <para>Formula: <c>variance = (Σ(xᵢ - μ)²) / n</c></para>
        /// <para>Note: All values are converted to <c>double</c> using <c>Convert.ToDouble</c> for calculation.</para>
        /// </summary>
        public static double Variance<T>(IEnumerable<T> data) where T : struct, IConvertible
        {
            var doubleData = data.Select(x => Convert.ToDouble(x)).ToList();
            var mean = doubleData.Average();
            return doubleData.Average(x => System.Math.Pow(x - mean, 2));
        }

        /// <summary>
        /// Calculates the standard deviation of a sequence of numbers.
        /// <para>Definition: The square root of the variance.</para>
        /// <para>Formula: <c>stddev = √variance</c></para>
        /// <para>Note: All values are converted to <c>double</c> using <c>Convert.ToDouble</c> for calculation.</para>
        /// </summary>
        public static double StandardDeviation<T>(IEnumerable<T> data) where T : struct, IConvertible
        {
            return System.Math.Sqrt(Variance(data));
        }

        /// <summary>
        /// Calculates the percentage increase from a starting value to a final value.
        /// </summary>
        /// <param name="startValue">The initial value.</param>
        /// <param name="finalValue">The final value after increase.</param>
        /// <returns>The percentage increase from <paramref name="startValue"/> to <paramref name="finalValue"/>.</returns>
        /// <example>
        /// double result = MathEx.PercentIncrease(100, 150); // Returns 50.0
        /// </example>
        public static double PercentIncrease(double startValue, double finalValue)
        {
            Guard.Against.Zero(startValue, exceptionCreator: () => throw new DivideByZeroException("The input cannot be zero."));
            var difference = finalValue - startValue;
            var percent = (difference / System.Math.Abs(startValue)) * 100;
            return percent;
        }

        /// <summary>
        /// Calculates the percent error between an experimental and a theoretical value.
        /// </summary>
        /// <param name="experimental">The experimentally observed or measured value.</param>
        /// <param name="theoretical">The theoretically expected or true value.</param>
        /// <returns>The percent error as a positive value.</returns>
        /// <example>
        /// double result = MathEx.PercentError(98, 100); // Returns 2.0
        /// </example>
        public static double PercentError(double experimental, double theoretical)
        {
            Guard.Against.Zero(theoretical, exceptionCreator: () => throw new DivideByZeroException("The input cannot be zero."));
            var difference = System.Math.Abs((experimental - theoretical) / theoretical);
            var percentError = difference * 100;
            return percentError;
        }

        /// <summary>
        /// Calculates the percentage change between two values.
        /// </summary>
        /// <param name="originalValue">The original value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The percentage change from <paramref name="originalValue"/> to <paramref name="newValue"/>.</returns>
        /// <example>
        /// double result = MathEx.PercentChange(80, 100); // Returns 25.0
        /// </example>
        public static double PercentChange(double originalValue, double newValue)
        {
            return ((newValue - originalValue) / System.Math.Abs(Guard.Against.Zero(originalValue, exceptionCreator: () => throw new DivideByZeroException("The input cannot be zero.")))) * 100;
        }

        /// <summary>
        /// Calculates the ratio between two values.
        /// </summary>
        /// <param name="part">The part value.</param>
        /// <param name="whole">The whole value.</param>
        /// <returns>The ratio as a value between 0 and 1.</returns>
        /// <example>
        /// double result = MathEx.Ratio(2, 5); // Returns 0.4
        /// </example>
        public static double Ratio(double part, double whole)
        {
            return whole == 0 ? 0 : part / whole;
        }

        /// <summary>
        /// Calculates the absolute difference between two numbers.
        /// </summary>
        /// <param name="value1">First number.</param>
        /// <param name="value2">Second number.</param>
        /// <returns>The absolute difference.</returns>
        /// <example>
        /// double result = MathEx.AbsoluteDifference(10, 6); // Returns 4.0
        /// </example>
        public static double AbsoluteDifference(double value1, double value2)
        {
            return System.Math.Abs(value1 - value2);
        }
    }
}