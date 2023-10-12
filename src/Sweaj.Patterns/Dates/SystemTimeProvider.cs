namespace Sweaj.Patterns.Dates
{
    public sealed class SystemTimeProvider : IDateTimeProvider
    {
        private DateTimeOffset? cachedDateTimeOffset;

        /// <summary>
        /// Every call to ExactNow may refer to a new time instant.
        /// </summary>
        /// <returns>A<see cref="DateTimeOffset.UtcNow"/> every call. </returns>
        public DateTimeOffset ExactNow()
        {
            return DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Initial call to <see cref="Now"/> is cached, for lifetime of object.
        /// </summary>
        /// <returns>A <see cref="DateTimeOffset.UtcNow"/>, but subsequent calls refer to same value from initial call.</returns>
        public DateTimeOffset Now()
        {
            if (this.cachedDateTimeOffset.HasValue)
                return this.cachedDateTimeOffset.Value;

            this.cachedDateTimeOffset = DateTimeOffset.UtcNow;
            return this.cachedDateTimeOffset.Value;
        }
    }
}
