using Sweaj.Patterns.Data.Services;
using Sweaj.Patterns.Dates;

namespace Sweaj.Patterns.Requests
{
    public abstract class Request
    {
        public Request(IDateTimeProvider dateTimeProvider)
        {
            RequestTime = dateTimeProvider.Now();
        }

        public DateTimeOffset RequestTime { get; }
    }
    public abstract class Request<T> : Request, IValueProvider<T>
    {
        protected Request([NotNull, ValidatedNotNull] T value, IDateTimeProvider dateTimeProvider)
            : base(dateTimeProvider)
        {
            Value = Guard.Against.Null(value);
        }

        public T Value { get; set; }
    }
}