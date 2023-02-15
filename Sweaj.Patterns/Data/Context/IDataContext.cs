using Sweaj.Patterns.Data.Entities;
using System.Linq.Expressions;

namespace Sweaj.Patterns.Data.Context
{
    public interface IDataContext
    {
        Task Include<TKey, TEntity, TProperty>(IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> navigationalPropertyPath)
            where TKey : IEquatable<TKey>, new()
            where TEntity : Entity<TKey>, IAggregateRoot;
        Task SaveChangesAsync();
    }

    /// <summary>
    /// Provides access to the actual underlying data context used for the implementation.
    /// <para>Provides access to the specific context to do custom operations on if needed.</para>
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IDataContext<TContext>
        where TContext : IDataContext
    {
        TContext Context { get; }
    }
}
