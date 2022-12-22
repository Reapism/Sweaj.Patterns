using Ardalis.GuardClauses;

namespace Sweaj.Patterns.Guards.Mathematics
{

    public sealed class NotEqualsException<TEqualsType> : Exception
    {
        public NotEqualsException(TEqualsType expectedValue, TEqualsType actualValue)
            : base(message: $"The actual value was [{expectedValue}]. Expected [{expectedValue}]")
        {
        }
    }
    public static class MathematicalEqualGuardExtensions
    {
        public static void Equals<TParam, TResult>(this IGuardClause guardClause, Func<TParam, TResult> mathFunction, TResult expectedResult, TParam param1)
            where TResult : IEquatable<TResult>
        {
            guardClause.Null(mathFunction, nameof(mathFunction));
            try
            {
                var actualResult = mathFunction(param1);

                if (!expectedResult.Equals(actualResult))
                {
                    throw new NotEqualsException<TResult>(expectedResult, actualResult);
                }
            }
            catch
            {
                throw;
            }
        }

        public static void NotEquals<TParam, TResult>(this IGuardClause guardClause, Func<TParam, TResult> mathFunction, TResult expectedResult, TParam param1)
        {
            guardClause.Null(mathFunction, nameof(mathFunction));
            try
            {
                var actualResult = mathFunction(param1);

                if (!expectedResult.Equals(actualResult))
                {
                    throw new NotEqualsException<TResult>(expectedResult, actualResult);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
