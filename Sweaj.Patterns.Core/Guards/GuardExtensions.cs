﻿using Ardalis.GuardClauses;

namespace Sweaj.Patterns.Guards
{

    public static class GuardExtensions
    {
        public static void Equals<T>(this IGuardClause guardClause, T expectedInput, T actualInput, string actualInputParameterName)
            where T : IEquatable<T>
        {
            if (expectedInput.Equals(actualInput))
            {
                throw new ArgumentException("Actual input was equal to the expected input.", actualInputParameterName);
            }
        }

        public static void Equals<T, W>(this IGuardClause guardClause, T expectedInput, T actualInput, string actualInputParameterName)
            where T : W, IEquatable<T>
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
