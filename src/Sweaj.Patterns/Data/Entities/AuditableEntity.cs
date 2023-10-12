namespace Sweaj.Patterns.Data.Entities
{
    internal interface IAuditableEntity
    {
        DateTimeOffset LastModifiedDate { get; }
        string LastModifiedByUser { get; }
        DateTimeOffset CreatedDate { get; }
        string CreatedByUser { get; }
    }

    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        public DateTimeOffset LastModifiedDate { get; private set; }
        public string LastModifiedByUser { get; private set; } = string.Empty;
        public DateTimeOffset CreatedDate { get; private set; }
        public string CreatedByUser { get; private set; } = string.Empty;

        public void Audit(string modifyingUser)
        {
            LastModifiedDate = DateTimeOffset.Now;
        }

        public void AuditNewUserModified(string modifyingUser)
        {

        }
    }
}
