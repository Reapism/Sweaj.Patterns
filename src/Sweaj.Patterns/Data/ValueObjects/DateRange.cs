namespace Sweaj.Patterns.Data.ValueObjects
{

    public sealed class DateRange
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        private DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public static DateRange Create(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new InvalidDateRangeException($"Invalid Date Range: Start date '{start.ToString("yyyy-MM-dd")}' is after end date '{end.ToString("yyyy-MM-dd")}'");
            }

            return new DateRange(start, end);
        }

        public static DateRange Create(DateTime start, TimeSpan duration)
        {
            var end = start.Add(duration);
            return Create(start, end);
        }

        public TimeSpan Duration()
        {
            return End - Start;
        }
    }
}
