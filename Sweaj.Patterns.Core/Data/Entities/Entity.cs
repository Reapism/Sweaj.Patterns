using Sweaj.Patterns.Data.Domain;
using Sweaj.Patterns.NullObject;

namespace Sweaj.Patterns.Data.Entities
{
    /// <summary>
    /// A base entity with a <see cref="Guid"/> type identifier and support for holding
    /// events.
    /// </summary>
    public abstract class Entity : Entity<Guid>
    {
        public override Entity<Guid> Empty()
        {
            return base.Empty();
        }
        public override bool IsEmpty()
        {
            return Guid.Empty.Equals(Id);
        }
    }

    /// <summary>
    /// A base entity with generic type identifier and support for holding
    /// events, and uses t.
    /// </summary>
    public class Entity<TKey> : IDomainEventProvider, IEmpty<Entity<TKey>>
        where TKey : IEquatable<TKey>, new()
    {
        public TKey Id { get; protected set; } = new();

        public List<DomainEvent> Events { get; } = new();
        public virtual bool IsEmpty() => Id.Equals(Empty().Id);

        public virtual Entity<TKey> Empty() => new();
    }

    public abstract class PolymorphicEntity : PolymorphicEntity<Guid>
    {
    }

    public abstract class PolymorphicEntity<TKey> : Entity<TKey>, IReferenceable<TKey>
        where TKey : IEquatable<TKey>, new()
    {
        public TKey ReferenceId { get; set; } = new();
        public string ReferenceName { get; set; } = string.Empty;

    }
}