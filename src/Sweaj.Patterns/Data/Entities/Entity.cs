﻿using Sweaj.Patterns.Attributes;
using Sweaj.Patterns.Data.Domain;
using Sweaj.Patterns.NullObject;

namespace Sweaj.Patterns.Data.Entities
{
    /// <summary>
    /// A base entity with a <see cref="Guid"/> key type identifier and support for holding
    /// events.
    /// </summary>
    [Trackable]
    public abstract class Entity : Entity<Guid>
    {
        public override bool IsEmpty()
        {
            return Guid.Empty.Equals(Id);
        }
    }

    /// <summary>
    /// A base entity with generic type identifier and support for holding
    /// events, and uses a generic key type.
    /// </summary>
    [Trackable]
    public abstract class Entity<TKey> : IEmpty
        where TKey : IEquatable<TKey>, new()
    {
        public TKey Id { get; protected set; } = new TKey();

        public List<DomainEvent> Events = new();

        public virtual bool IsEmpty() => Id.Equals(default);
    }
}