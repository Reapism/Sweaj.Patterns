using System.Reflection;

namespace Sweaj.Patterns.Attributes
{
    public static class AttributeRetriever
    {

        public static IEnumerable<TAttribute> FilterBy<TAttribute>(this Assembly assembly, [CanBeNull] Func<TAttribute, bool>? predicate)
            where TAttribute : Attribute
        {
            var attributes = assembly.GetCustomAttributes<TAttribute>();
            var filteredAttributesByCriteria = predicate is null ? attributes : attributes.Where(predicate).ToArray();

            return filteredAttributesByCriteria;
        }
        /// <summary>
        /// Gathers all attributes from an array of assembiles, 
        /// and filters them by a particular predicate.
        /// </summary>
        /// <typeparam name="TAttribute">An attribute to search and filter by.</typeparam>
        /// <param name="assembliesToSearch">An array of assemblies.</param>
        /// <param name="predicate">The predicate to filter by. If null, gets all attributes.</param>
        /// <returns></returns>
        public static IEnumerable<TAttribute> FilterBy<TAttribute>(this Assembly[] assembliesToSearch, [CanBeNull] Func<TAttribute, bool>? predicate)
            where TAttribute : Attribute
        {
            var allAttributes = new List<TAttribute>();

            foreach (var assembly in assembliesToSearch)
            {
                var attributes = assembly.GetCustomAttributes<TAttribute>();
                allAttributes.AddRange(attributes);
            }

            var filteredAttributesByCriteria = predicate is null ? allAttributes : allAttributes.Where(predicate).ToList();

            return filteredAttributesByCriteria;
        }
    }
}
