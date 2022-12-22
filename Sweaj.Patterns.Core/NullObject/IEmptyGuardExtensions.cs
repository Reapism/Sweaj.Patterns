using Sweaj.Patterns.ValidateClause;

namespace Sweaj.Patterns.NullObject
{
    internal static class IEmptyGuardExtensions
    {
#pragma warning disable IDE0060 // Remove unused parameter
        public static T NullOrEmpty<T>(this IGuardClause guardClause, T emptyableValue, string parameterName)
            where T : IEmpty<T>
        {
            if (emptyableValue is null || emptyableValue.IsEmpty())
            {
                throw new ArgumentNullException(parameterName);
            }
            return emptyableValue;
        }
#pragma warning restore IDE0060 // Remove unused parameter
    }
}