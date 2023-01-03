using System.Linq.Expressions;
using System.Reflection;

namespace Sweaj.Patterns.Reflection
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<FieldInfo> GetFieldsBy<T>(BindingFlags bindingAttrs, Func<FieldInfo, bool> filter)
        {
            return typeof(T).GetFields(bindingAttrs).Where(filter);
        }

        public static IEnumerable<PropertyInfo> GetPropertiesBy<T>(BindingFlags bindingAttrs, Func<PropertyInfo, bool> filter)
        {
            return typeof(T).GetProperties(bindingAttrs).Where(filter);
        }

        public static IEnumerable<MemberInfo> GetMembersBy<T>(BindingFlags bindingAttrs, Func<MemberInfo, bool> filter)
        {
            return typeof(T).GetMembers(bindingAttrs).Where(filter);
        }
    }


}
