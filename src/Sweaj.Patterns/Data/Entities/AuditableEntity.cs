using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Data.Entities
{
    [Trackable]
    internal interface IAuditableEntity
    {
        DateTimeOffset LastModifiedDate { get; }
        string LastModifiedByUser { get; }
        DateTimeOffset CreatedDate { get; }
        string CreatedByUser { get; }
    }

    [Trackable]
    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        public DateTimeOffset LastModifiedDate { get; private set; } = DateTimeOffset.UtcNow;
        public string LastModifiedByUser { get; private set; } = string.Empty;
        public DateTimeOffset CreatedDate { get; private set; } = DateTimeOffset.UtcNow;
        public string CreatedByUser { get; private set; } = string.Empty;

        public void Audit(string modifyingUser)
        {
            LastModifiedDate = DateTimeOffset.UtcNow;
            LastModifiedByUser = modifyingUser;
        }

        public void AuditNewUserModified(string createdByUser)
        {
            CreatedByUser = createdByUser;
            CreatedDate = DateTimeOffset.UtcNow;
            Audit(createdByUser);
        }
    }
    /// <summary>
    /// Marks a type as being an entity with a custom key type with audit properties.
    /// <para>Use this abstraction if you want specify a different key.</para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    [Trackable]
    public abstract class AuditableEntity<TKey> : Entity<TKey>, IAuditableEntity
        where TKey : IEquatable<TKey>, new()
    {
        public DateTimeOffset LastModifiedDate { get; private set; } = DateTimeOffset.UtcNow;
        public string LastModifiedByUser { get; private set; } = string.Empty;
        public DateTimeOffset CreatedDate { get; private set; } = DateTimeOffset.UtcNow;
        public string CreatedByUser { get; private set; } = string.Empty;

        public void Audit(string modifyingUser)
        {
            LastModifiedDate = DateTimeOffset.UtcNow;
            LastModifiedByUser = modifyingUser;
        }

        public void AuditNewUserModified(string createdByUser)
        {
            CreatedByUser = createdByUser;
            CreatedDate = DateTimeOffset.UtcNow;
            Audit(createdByUser);
        }
    }
}
