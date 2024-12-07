using System.Reflection;

namespace Sweaj.Patterns.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetTypesWithAttribute<TAttribute>([NotNull] this Assembly assembly, [CanBeNull] Func<TAttribute, Type, bool> filter = null)
            where TAttribute : Attribute
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var result = new List<Type>();

            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsPublic)
                {
                    continue;
                }

                var attribute = type.GetCustomAttribute<TAttribute>();
                if (attribute == null)
                {
                    continue;
                }

                if (filter == null || filter(attribute, type))
                {
                    result.Add(type);
                }
            }

            return result;
        }

        public static IEnumerable<Type> GetTypesWithAttribute<TAttribute>([NotNull] this IEnumerable<Assembly> assemblies, [CanBeNull] Func<TAttribute, Type, bool> filter = null)
            where TAttribute : Attribute
        {
            var types = new List<Type>();

            foreach (var assembly in assemblies)
            {
                types.AddRange(GetTypesWithAttribute(assembly, filter));
            }

            return types;
        }
    }
}