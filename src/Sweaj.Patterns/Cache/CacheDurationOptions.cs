namespace Sweaj.Patterns.Cache
{
    public sealed class CacheDurationOptions
    {
        private CacheDurationOptions([CanBeNull] DateTimeOffset? absoluteExpiration, [CanBeNull] TimeSpan? absoluteExpirationRelativeToNow, [CanBeNull] TimeSpan? slidingExpiration)
        {
            AbsoluteExpiration = absoluteExpiration;
            AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
            SlidingExpiration = slidingExpiration;
        }

        public DateTimeOffset? AbsoluteExpiration { get; }
        public TimeSpan? AbsoluteExpirationRelativeToNow { get; }
        public TimeSpan? SlidingExpiration { get; }

        public static CacheDurationOptions FromAbsoluteExpiration(DateTimeOffset absoluteExpiration)
        {
            return new CacheDurationOptions(absoluteExpiration, null, null);
        }

        public static CacheDurationOptions FromAbsoluteExpiration(DateTimeOffset absoluteExpiration, TimeSpan slidingExpiration)
        {
            return new CacheDurationOptions(absoluteExpiration, null, slidingExpiration);
        }

        public static CacheDurationOptions FromRelativeToNowExpiration(TimeSpan absoluteExpirationRelativeToNow)
        {
            return new CacheDurationOptions(null, absoluteExpirationRelativeToNow, null);
        }

        public static CacheDurationOptions FromRelativeToNowExpiration(TimeSpan absoluteExpirationRelativeToNow, TimeSpan slidingExpiration)
        {
            return new CacheDurationOptions(null, absoluteExpirationRelativeToNow, slidingExpiration);
        }
    }
}
