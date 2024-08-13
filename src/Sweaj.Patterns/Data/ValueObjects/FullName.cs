namespace Sweaj.Patterns.Data.ValueObjects
{
    /// <summary>
    /// An object that represents a fully constructed name that is validated upon creation .
    /// </summary>
    public sealed class FullName
    {
        public string FirstName { get; }
        public string? MiddleName { get; }
        public string LastName { get; }

        public FullName(string firstName, string lastName)
            : this(firstName, string.Empty, lastName)
        {
        }

        public FullName(string firstName, string? middleName, string lastName)
        {
            FirstName = Guard.Against.NullOrWhiteSpace(firstName);
            MiddleName = middleName ?? string.Empty;
            LastName = Guard.Against.NullOrWhiteSpace(lastName);
        }

        public override string ToString()
        {
            return $"{FirstName} {(string.IsNullOrEmpty(MiddleName) ? string.Empty : $"{MiddleName} ")}{LastName}";
        }
    }
}
