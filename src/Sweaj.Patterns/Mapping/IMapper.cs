using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Mapping
{
    /// <summary>
    /// This should be relied upon when doing a entity to value 
    /// mapping as opposed to an abstraction created from another library.
    /// </summary>
    [Trackable]
    public interface IMapper
    {
        /// <summary>
        /// The mechanism used to convert an source value into a destination value.
        /// </summary>
        /// <typeparam name="TSource">The type to convert from.</typeparam>
        /// <typeparam name="TDestination">The type to convert to.</typeparam>
        /// <param name="source">The <typeparamref name="TSource"/> type used to construct a <typeparamref name="TDestination"/>.</param>
        /// <returns></returns>
        TDestination Convert<TSource, TDestination>(TSource source);
    }
}
