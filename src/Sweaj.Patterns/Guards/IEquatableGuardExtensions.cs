﻿using Sweaj.Patterns.NullObject;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Sweaj.Patterns.Guards
{
    public static class IEquatableGuardExtensions
    {
        public static BigInteger NegativeOrZero(this IGuardClause guardClause, BigInteger value, [CallerMemberName] string parameterName = "")
        {
            if (BigInteger.IsNegative(value) || BigInteger.Zero == value)
            {
                throw new ArgumentException($"The value {value} must be greater than zero.");
            }

            return value;
        }

        public static T NullOrEmpty<T>(this IGuardClause guardClause, T value, [CallerMemberName] string parameterName = "")
            where T : IEmpty
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName, "The provided value is null.");
            }

            if (value.IsEmpty())
            {
                throw new ArgumentException("The provided value is empty.");
            }

            return value;
        }

        public static void Equals<T>(this IGuardClause guardClause, T expectedInput, T actualInput, string actualInputParameterName)
            where T : IEquatable<T>
        {
            if (expectedInput.Equals(actualInput))
            {
                throw new ArgumentException("Actual input was equal to the expected input.", actualInputParameterName);
            }
        }

        public static void NotEquals<T>(this IGuardClause guardClause, T expectedInput, T actualInput, string actualInputParameterName)
            where T : IEquatable<T>
        {
            if (expectedInput.Equals(actualInput))
            {
                throw new ArgumentException("Actual input was not equal to the expected input.", actualInputParameterName);
            }
        }
    }
}
