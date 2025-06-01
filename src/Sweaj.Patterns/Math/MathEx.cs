namespace Sweaj.Patterns.Math;

/// <summary>
/// A set of extended mathematical formulas for common analytical computations such as percentage calculations, growth ratios, and comparative differences.
/// </summary>
/// <remarks>
/// This static class provides utilities typically needed in data analysis, financial calculations, and error estimation.
/// </remarks>
public static class MathEx
{
    public static class Algebra
    {

    }
    /// <summary>
    /// Provides geometric calculations such as area and perimeter for basic shapes.
    /// </summary>
    public static class Geometry
    {
        /// <summary>
        /// Calculates the area of a rectangle.
        /// <para>Formula: <c>length × width</c></para>
        /// </summary>
        public static double AreaOfRectangle(double length, double width) => length * width;

        /// <summary>
        /// Calculates the perimeter of a rectangle.
        /// <para>Formula: <c>2 × (length + width)</c></para>
        /// </summary>
        public static double PerimeterOfRectangle(double length, double width) => 2 * (length + width);

        /// <summary>
        /// Calculates the area of a circle.
        /// <para>Formula: <c>π × radius²</c></para>
        /// </summary>
        public static double AreaOfCircle(double radius) => System.Math.PI * radius * radius;

        /// <summary>
        /// Calculates the circumference (perimeter) of a circle.
        /// <para>Formula: <c>2 × π × radius</c></para>
        /// </summary>
        public static double CircumferenceOfCircle(double radius) => 2 * System.Math.PI * radius;

        /// <summary>
        /// Calculates the area of a triangle given its base and height.
        /// <para>Formula: <c>½ × base × height</c></para>
        /// </summary>
        public static double AreaOfTriangle(double baseLength, double height) => 0.5 * baseLength * height;
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
        return ((newValue - originalValue) / System.Math.Abs(originalValue)) * 100;
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
