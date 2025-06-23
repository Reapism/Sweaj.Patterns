using System.Numerics;

namespace Sweaj.Patterns.Math;

public static partial class MathExt
{
    public static partial class Algebra
    {
        /// <summary>
        /// Multiplies two matrices and returns the resulting matrix.
        /// </summary>
        /// <param name="matrixA">The first matrix (m x n).</param>
        /// <param name="matrixB">The second matrix (n x p).</param>
        /// <returns>The resulting matrix after multiplication (m x p).</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the number of columns in <paramref name="matrixA"/> 
        /// does not match the number of rows in <paramref name="matrixB"/>.
        /// </exception>
        /// <example>
        /// <code>
        /// var A = new int[,] { {1, 2}, {3, 4} };
        /// var B = new int[,] { {2, 0}, {1, 2} };
        /// var result = MatrixOperations.Multiply(A, B);
        /// // result = { {4, 4}, {10, 8} }
        /// </code>
        /// </example>
        /// <remarks>
        /// Matrix multiplication is only defined when the number of columns in the first matrix 
        /// equals the number of rows in the second matrix.
        /// </remarks>
        public static int[,] Multiply(int[,] matrixA, int[,] matrixB)
        {
            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int rowsB = matrixB.GetLength(0);
            int colsB = matrixB.GetLength(1);

            if (colsA != rowsB)
            {
                throw new ArgumentException("Number of columns in matrixA must equal number of rows in matrixB.");
            }

            int[,] result = new int[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        sum += matrixA[i, k] * matrixB[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }

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
}