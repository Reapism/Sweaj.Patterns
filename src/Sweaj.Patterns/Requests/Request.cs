using Sweaj.Patterns.NullObject;

namespace Sweaj.Patterns.Requests
{
    public abstract class Request<T> : IEmpty
    {
        protected Request([NotNull, ValidatedNotNull] T? value, DateTimeOffset requestTime)
        {
            Value = Guard.Against.Null(value);
            RequestTime = requestTime;
        }

        public T? Value { get; set; }
        public DateTimeOffset RequestTime { get; }
        public bool IsEmpty()
        {
            return Value is null;
        }
    }
}