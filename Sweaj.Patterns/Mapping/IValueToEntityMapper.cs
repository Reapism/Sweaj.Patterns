namespace Sweaj.Patterns.Mapping
{
    public interface IValueToEntityMapper<TValue, TEntity>
    {
        TEntity Convert(TValue value);
    }
}
