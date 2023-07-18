using Sweaj.Patterns.NullObject;

namespace Sweaj.Patterns.Requests
{
    public class Request<T> : IEmpty
    {
        private Request(T? value, DateTimeOffset requestTime)
        {
            Value = value;
            RequestTime = requestTime;
        }

        public T? Value { get; set; }
        public DateTimeOffset RequestTime { get; }
        public bool IsEmpty()
        {
            return Value is null;
        }

        public static Request<T> Create([NotNull, ValidatedNotNull] T value)
        {

            return new Request<T>(Guard.Against.Null(value), DateTimeOffset.UtcNow);
        }

        public static Request<T> Empty()
        {
            return new Request<T>(default, DateTimeOffset.UtcNow);
        }
    }
}
