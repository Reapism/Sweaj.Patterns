namespace Sweaj.Patterns.Mapping
{
    public interface IEntityToValueMapper<TEntity, TValue>
    {
        TValue Convert(TEntity entity);
    }
}
