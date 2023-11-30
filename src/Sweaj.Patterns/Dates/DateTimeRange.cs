namespace Sweaj.Patterns.Dates
{
    public sealed class DateTimeRange
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        private DateTimeRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public static DateTimeRange Create(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new InvalidDateRangeException($"Invalid Date Range: Start date '{start.ToString("yyyy-MM-dd")}' is after end date '{end.ToString("yyyy-MM-dd")}'");
            }

            return new DateTimeRange(start, end);
        }

        public static DateTimeRange Create(DateTime start, TimeSpan duration)
        {
            var end = start.Add(
                Guard.Against.AgainstExpression
                (
                    (dur) => TimeSpan.Zero == dur, duration, "Duration must be positive.")
                );
            return Create(start, end);
        }

        public TimeSpan Duration()
        {
            return End - Start;
        }
    }

    public sealed class DateTimeOffsetRange
    {
        public DateTimeOffset Start { get; }
        public DateTimeOffset End { get; }

        private DateTimeOffsetRange(DateTimeOffset start, DateTimeOffset end)
        {
            Start = start;
            End = end;
        }

        public static DateTimeOffsetRange Create(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new InvalidDateRangeException($"Invalid Date Range: Start date '{start.ToLongDateString}' cannot be after end date '{end.ToLongDateString}'");
            }

            return new DateTimeOffsetRange(start, end);
        }

        public static DateTimeOffsetRange Create(DateTime start, TimeSpan duration)
        {
            var end = start.Add(
                Guard.Against.AgainstExpression
                (
                    (dur) => TimeSpan.Zero == dur, duration, "Duration must be positive.")
                );
            return Create(start, end);
        }

        public TimeSpan Duration()
        {
            return End - Start;
        }
    }
}
