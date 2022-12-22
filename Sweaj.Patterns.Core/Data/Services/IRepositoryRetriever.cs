using Sweaj.Patterns.Data.Entities;
using Sweaj.Patterns.Data.Repository;

namespace Sweaj.Patterns.Data.Services
{
    public interface IRepositoryRetriever
    {
        IRepository GetRepository();
    }

    public interface IRepositoryRetriever<TEntity>
        where TEntity : Entity, IAggregateRoot
    {
        IRepository<TEntity> GetRepository();
    }

    public interface IRepositoryRetriever<TKey, TEntity>
        where TKey : IEquatable<TKey>, new()
        where TEntity : Entity<TKey>, IAggregateRoot
    {
        IRepository<TKey, TEntity> GetRepository();
    }
}
