namespace Sweaj.Patterns.Dates
{
    public sealed class MockedTimeProvider : IDateTimeProvider
    {
        private readonly DateTimeOffset mockedDateTimeOffset;

        public MockedTimeProvider(DateTimeOffset mockedDateTimeOffset)
        {
            this.mockedDateTimeOffset = mockedDateTimeOffset;
        }

        public DateTimeOffset ExactNow()
        {
            throw new NotImplementedException();
        }

        public DateTimeOffset Now()
        {
            return mockedDateTimeOffset;
        }
    }
}
