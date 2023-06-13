namespace Sweaj.Patterns.Data.Entities
{
    public interface IAuditableEntity
    {
        DateTimeOffset LastModifiedDate { get; set; }
        string LastModifiedByUser { get; set; }
        DateTimeOffset CreatedDate { get; set; }
        string CreatedByUser { get; set; }
    }

    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        public DateTimeOffset LastModifiedDate { get; set; }
        public string LastModifiedByUser { get; set; } = string.Empty;
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedByUser { get; set; } = string.Empty;
    }
}
