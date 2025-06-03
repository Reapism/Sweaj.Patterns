namespace Sweaj.Patterns.Data.ValueObjects;

/// <summary>
/// Represents a person's full name, including first, middle (optional), and last names.
/// This value object ensures that first and last names are not null or whitespace.
/// </summary>
/// <remarks>
/// The middle name is optional and stored as an empty string if not provided.
/// </remarks>
public sealed class FullName
{
    /// <summary>
    /// Gets the first name of the person.
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// Gets the middle name of the person, or an empty string if none is provided.
    /// </summary>
    public string? MiddleName { get; }

    /// <summary>
    /// Gets the last name of the person.
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FullName"/> class with the specified first and last names.
    /// </summary>
    /// <param name="firstName">The first name of the person.</param>
    /// <param name="lastName">The last name of the person.</param>
    /// <exception cref="System.ArgumentException">Thrown if <paramref name="firstName"/> or <paramref name="lastName"/> is null or whitespace.</exception>
    /// <example>
    /// var name = new FullName("John", "Doe");
    /// </example>
    public FullName(string firstName, string lastName)
        : this(firstName, string.Empty, lastName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FullName"/> class with the specified first, middle, and last names.
    /// </summary>
    /// <param name="firstName">The first name of the person.</param>
    /// <param name="middleName">The middle name of the person. Defaults to an empty string if null.</param>
    /// <param name="lastName">The last name of the person.</param>
    /// <exception cref="System.ArgumentException">Thrown if <paramref name="firstName"/> or <paramref name="lastName"/> is null or whitespace.</exception>
    /// <example>
    /// var name = new FullName("John", "F.", "Doe");
    /// </example>
    public FullName(string firstName, string? middleName, string lastName)
    {
        FirstName = Guard.Against.NullOrWhiteSpace(firstName);
        MiddleName = middleName ?? string.Empty;
        LastName = Guard.Against.NullOrWhiteSpace(lastName);
    }

    /// <summary>
    /// Returns the full name as a single formatted string.
    /// </summary>
    /// <returns>A string containing the full name in the format "First Middle Last".</returns>
    /// <example>
    /// var name = new FullName("Jane", "A.", "Smith");
    /// Console.WriteLine(name.ToString()); // Output: "Jane A. Smith"
    /// </example>
    public override string ToString()
    {
        return $"{FirstName} {(string.IsNullOrEmpty(MiddleName) ? string.Empty : $"{MiddleName} ")}{LastName}";
    }
}
