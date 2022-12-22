namespace Sweaj.Patterns.Data.Entities
{
    public interface IAuditableEntity
    {
        DateTime LastModifiedDate { get; set; }
        string LastModifiedByUser { get; set; }
        DateTime CreatedDate { get; set; }
        string CreatedByUser { get; set; }
    }

    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedByUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string CreatedByUser { get; set; } = string.Empty;
    }
}
