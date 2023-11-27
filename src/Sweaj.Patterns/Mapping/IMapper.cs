namespace Sweaj.Patterns.Mapping
{
    /// <summary>
    /// This should be relied upon when doing a entity to value 
    /// mapping as opposed to an abstraction created from another library.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public interface IMapper<TSource, TDestination>
    {
        /// <summary>
        /// The mechanism used to convert an entity into a value.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TDestination Convert(TSource entity);
    }
}
