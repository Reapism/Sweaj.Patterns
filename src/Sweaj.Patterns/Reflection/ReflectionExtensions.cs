using System.Reflection;

namespace Sweaj.Patterns.Reflection
{
    /// <summary>
    /// Provides extension methods for reflection-based type inspection.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Gets the fields of type <typeparamref name="T"/> filtered by binding flags and a predicate.
        /// </summary>
        /// <typeparam name="T">The type whose fields to retrieve.</typeparam>
        /// <param name="bindingAttrs">Binding flags to control visibility and scope.</param>
        /// <param name="filter">A predicate to filter the fields.</param>
        /// <returns>An enumerable of matching <see cref="FieldInfo"/> objects.</returns>
        public static IEnumerable<FieldInfo> GetFieldsBy<T>(BindingFlags bindingAttrs, Func<FieldInfo, bool> filter)
        {
            return typeof(T).GetFields(bindingAttrs).Where(filter);
        }

        /// <summary>
        /// Gets the properties of type <typeparamref name="T"/> filtered by binding flags and a predicate.
        /// </summary>
        public static IEnumerable<PropertyInfo> GetPropertiesBy<T>(BindingFlags bindingAttrs, Func<PropertyInfo, bool> filter)
        {
            return typeof(T).GetProperties(bindingAttrs).Where(filter);
        }

        /// <summary>
        /// Gets the members of type <typeparamref name="T"/> filtered by binding flags and a predicate.
        /// </summary>
        public static IEnumerable<MemberInfo> GetMembersBy<T>(BindingFlags bindingAttrs, Func<MemberInfo, bool> filter)
        {
            return typeof(T).GetMembers(bindingAttrs).Where(filter);
        }

        /// <summary>
        /// Gets all constant fields defined in type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to reflect on.</typeparam>
        /// <returns>A list of constant field values.</returns>
        public static IEnumerable<object?> GetConstsValues<T>()
        {
            var type = typeof(T);
            return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(f => f.IsLiteral && !f.IsInitOnly)
                .Select(f => f.GetRawConstantValue());
        }


        /// <summary>
        /// Gets all constant field names defined in type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to reflect on.</typeparam>
        /// <returns>A list of constant field names.</returns>
        public static Dictionary<string, object?> GetConstsNames<T>()
        {
            var type = typeof(T);
            return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(f => f.IsLiteral && !f.IsInitOnly)
                .OrderBy(f => f.Name)
                .ToDictionary(k => k.Name, v => v.GetRawConstantValue());
        }

        /// <summary>
        /// Gets all members defined in type <typeparamref name="T"/> using specified binding flags.
        /// </summary>
        /// <param name="bindingAttrs">Binding flags to control which members are returned.</param>
        /// <typeparam name="T">The type to inspect.</typeparam>
        /// <returns>All matching members of the type.</returns>
        public static IEnumerable<MemberInfo> GetMembers<T>(BindingFlags bindingAttrs)
        {
            return typeof(T).GetMembers(bindingAttrs);
        }

        /// <summary>
        /// Gets all members of type <typeparamref name="T"/> that are decorated with a specific attribute type.
        /// </summary>
        /// <typeparam name="T">The type whose members to inspect.</typeparam>
        /// <typeparam name="TAttr">The type of attribute to search for.</typeparam>
        /// <param name="bindingAttrs">Binding flags to control scope and visibility.</param>
        /// <returns>An enumerable of members having the specified attribute.</returns>
        public static IEnumerable<MemberInfo> GetAttributeBy<T, TAttr>(BindingFlags bindingAttrs) where TAttr : Attribute
        {
            return typeof(T).GetMembers(bindingAttrs)
                .Where(m => m.GetCustomAttribute<TAttr>() != null);
        }
    }
}
