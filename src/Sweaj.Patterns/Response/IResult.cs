using Sweaj.Patterns.NullObject;

namespace Sweaj.Patterns.Response
{
    public interface IResult
    {
        Guid Id { get; }
    }

    public interface IResult<T> : IResult
    {
        T Result { get; }
    }
}
