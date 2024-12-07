using Sweaj.Patterns.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Sweaj.Patterns.Tests.Extensions
{
    // Sample attribute
    [AttributeUsage(AttributeTargets.Class)]
    public class SampleAttribute : Attribute
    {
        public string Name { get; set; }

        public SampleAttribute(string name)
        {
            Name = name;
        }
    }

    // Test classes
    [Sample("ClassA")]
    public class ClassA { }

    [Sample("ClassB")]
    public class ClassB { }

    public class ClassWithoutAttribute { }

    [Sample("FilteredClass")]
    public class FilteredClass { }

    public class TypeExtensionsTests
    {
        private readonly Assembly _currentAssembly = typeof(ClassA).Assembly;
        private readonly IEnumerable<Assembly> _assemblies;

        public TypeExtensionsTests()
        {
            _assemblies = [_currentAssembly];
        }

        // Theory-based test for verifying types with attributes (for single assembly)
        [Theory]
        [InlineData(typeof(ClassA), true)]
        [InlineData(typeof(ClassB), true)]
        [InlineData(typeof(ClassWithoutAttribute), false)]
        public void GetTypesWithAttribute_ShouldReturnTypesWithSpecifiedAttribute(Type type, bool isExpectedToBeIncluded)
        {
            // Act
            var types = _currentAssembly.GetTypesWithAttribute<SampleAttribute>();

            // Assert
            if (isExpectedToBeIncluded)
            {
                Assert.Contains(type, types);
            }
            else
            {
                Assert.DoesNotContain(type, types);
            }
        }

        // Theory-based test for verifying types with attributes (for multiple assemblies)
        [Theory]
        [InlineData(typeof(ClassA), true)]
        [InlineData(typeof(ClassB), true)]
        [InlineData(typeof(ClassWithoutAttribute), false)]
        public void GetTypesWithAttribute_FromAssemblies_ShouldReturnTypesWithSpecifiedAttribute(Type type, bool isExpectedToBeIncluded)
        {
            // Act
            var types = _assemblies.GetTypesWithAttribute<SampleAttribute>();

            // Assert
            if (isExpectedToBeIncluded)
            {
                Assert.Contains(type, types);
            }
            else
            {
                Assert.DoesNotContain(type, types);
            }
        }

        // Theory-based test for filter scenarios (for single assembly)
        [Theory]
        [InlineData("FilteredClass", typeof(FilteredClass))]
        [InlineData("ClassA", typeof(ClassA))]
        [InlineData("NonExistent", null)]
        public void GetTypesWithAttribute_ShouldApplyFilterCorrectly(string attributeName, Type expectedType)
        {
            // Act
            var types = _currentAssembly.GetTypesWithAttribute<SampleAttribute>(
                (attribute, type) => attribute.Name == attributeName);

            // Assert
            if (expectedType != null)
            {
                Assert.Contains(expectedType, types);
            }
            else
            {
                Assert.Empty(types);
            }
        }

        // Theory-based test for filter scenarios (for multiple assemblies)
        [Theory]
        [InlineData("FilteredClass", typeof(FilteredClass))]
        [InlineData("ClassA", typeof(ClassA))]
        [InlineData("NonExistent", null)]
        public void GetTypesWithAttribute_FromAssemblies_ShouldApplyFilterCorrectly(string attributeName, Type expectedType)
        {
            // Act
            var types = _assemblies.GetTypesWithAttribute<SampleAttribute>(
                (attribute, type) => attribute.Name == attributeName);

            // Assert
            if (expectedType != null)
            {
                Assert.Contains(expectedType, types);
            }
            else
            {
                Assert.Empty(types);
            }
        }

        // Fact for testing when no types have the specified attribute (for multiple assemblies)
        [Fact]
        public void GetTypesWithAttribute_FromAssemblies_ShouldReturnEmpty_WhenNoTypesHaveAttribute()
        {
            // Act
            var types = _assemblies.GetTypesWithAttribute<ObsoleteAttribute>();

            // Assert
            Assert.Empty(types);
        }

        // Private class to simulate no public types
        private class PrivateClass { }
    }
}
