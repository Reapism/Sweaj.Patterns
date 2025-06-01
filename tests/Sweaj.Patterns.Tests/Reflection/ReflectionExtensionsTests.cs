using Sweaj.Patterns.Reflection;
using System.Reflection;

namespace Sweaj.Patterns.Tests.Reflection;


public class ReflectionExtensionsTests
{
    private class SampleClass
    {
        public const string ConstA = "A";
        public const int ConstB = 42;
        public static string StaticField = "static";
        public string InstanceField = "instance";
        public int PropertyA { get; set; }

        [Obsolete]
        public void AnnotatedMethod() { }

        public void RegularMethod() { }
    }

    [Fact]
    public void GetFieldsBy_ReturnsMatchingFields()
    {
        var fields = ReflectionExtensions.GetFieldsBy<SampleClass>(
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static,
            f => f.IsStatic);

        Assert.Contains(fields, f => f.Name == nameof(SampleClass.StaticField));
        Assert.DoesNotContain(fields, f => f.Name == nameof(SampleClass.InstanceField));
    }

    [Fact]
    public void GetPropertiesBy_ReturnsAllProperties()
    {
        var properties = ReflectionExtensions.GetPropertiesBy<SampleClass>(
            BindingFlags.Public | BindingFlags.Instance,
            p => true);

        Assert.Contains(properties, p => p.Name == nameof(SampleClass.PropertyA));
    }

    [Fact]
    public void GetMembersBy_ReturnsMethodsWithFilter()
    {
        var members = ReflectionExtensions.GetMembersBy<SampleClass>(
            BindingFlags.Public | BindingFlags.Instance,
            m => m.MemberType == MemberTypes.Method);

        Assert.Contains(members, m => m.Name == nameof(SampleClass.AnnotatedMethod));
    }

    [Fact]
    public void GetConstsValues_ReturnsAllConstValues()
    {
        var consts = ReflectionExtensions.GetConstsValues<SampleClass>().ToList();
        Assert.Contains("A", consts);
        Assert.Contains(42, consts);
    }

    [Fact]
    public void GetConstsNames_ReturnsAllConstNamesWithValues()
    {
        var consts = ReflectionExtensions.GetConstsNames<SampleClass>();
        Assert.True(consts.ContainsKey(nameof(SampleClass.ConstA)));
        Assert.Equal("A", consts[nameof(SampleClass.ConstA)]);
        Assert.True(consts.ContainsKey(nameof(SampleClass.ConstB)));
        Assert.Equal(42, consts[nameof(SampleClass.ConstB)]);
    }

    [Fact]
    public void GetMembers_ReturnsAllPublicMembers()
    {
        var members = ReflectionExtensions.GetMembers<SampleClass>(BindingFlags.Public | BindingFlags.Instance);
        Assert.Contains(members, m => m.Name == nameof(SampleClass.PropertyA));
    }

    [Fact]
    public void GetAttributeBy_ReturnsMembersWithAttribute()
    {
        var members = ReflectionExtensions.GetAttributeBy<SampleClass, ObsoleteAttribute>(
            BindingFlags.Public | BindingFlags.Instance);

        Assert.Contains(members, m => m.Name == nameof(SampleClass.AnnotatedMethod));
    }
}
