using Sweaj.Patterns.NullObject;

namespace Sweaj.Patterns.Requests
{
    public class Request<T> : IEmpty<T>
        where T : new()
    {
        public T? Value { get; set; }
        public DateTimeOffset RequestTime { get; } = DateTimeOffset.Now;

        public T Empty()
        {
            return new();
        }

        public bool IsEmpty()
        {
            return Value is null;
        }
    }
}
