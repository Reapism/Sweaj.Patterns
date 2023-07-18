namespace Sweaj.Patterns.Dates
{
    public class MockedTimeProvider : ITimeProvider
    {
        private readonly DateTimeOffset mockedDateTimeOffset;

        public MockedTimeProvider(DateTimeOffset mockedDateTimeOffset)
        {
            this.mockedDateTimeOffset = mockedDateTimeOffset;
        }

        public DateTimeOffset GetCurrentUtcTime()
        {
            return mockedDateTimeOffset;
        }
    }
}
