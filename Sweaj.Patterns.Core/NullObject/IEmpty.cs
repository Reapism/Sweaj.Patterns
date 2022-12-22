namespace Sweaj.Patterns.NullObject
{
    public interface IEmpty<T>
    {
        T Empty();
        bool IsEmpty();
    }
}
