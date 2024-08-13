using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.NullObject
{
    /// <summary>
    /// A mechanism for checking if a type has an empty state.
    /// </summary>
    [Trackable]
    public interface IEmpty
    {
        /// <summary>
        /// Returns an empty instance.
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();
    }
}
