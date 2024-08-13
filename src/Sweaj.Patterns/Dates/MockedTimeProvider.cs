namespace Sweaj.Patterns.Dates
{
    /// <summary>
    /// Allows the consumer to pass a date of their choicing for the <see cref="Now"/>
    /// call.
    /// </summary>
    public sealed class MockedTimeProvider : IDateTimeProvider
    {
        private readonly DateTimeOffset mockedDateTimeOffset;

        public MockedTimeProvider(DateTimeOffset mockedDateTimeOffset)
        {
            this.mockedDateTimeOffset = mockedDateTimeOffset;
        }

        public DateTimeOffset Now()
        {
            return this.mockedDateTimeOffset;
        }
    }
}
