using Sweaj.Patterns.Data.Values;
using Sweaj.Patterns.Rest.Response;

namespace Sweaj.Patterns.Tests.Rest.Responses
{
    public class ResultTests
    {
        [Fact]
        public void Create_WithValue_SetsPropertiesCorrectly()
        {
            var value = "test";
            var result = Result<string>.Create(value);

            Assert.Equal(value, result.Value);
            Assert.NotEqual(Guid.Empty, result.ResultId);
        }

        private class TestValueProvider : IValueProvider<string>
        {
            public string Value => "from provider";
        }

        [Fact]
        public void Create_WithValueProvider_SetsPropertiesCorrectly()
        {
            var provider = new TestValueProvider();
            var result = Result<string>.Create(provider);

            Assert.Equal(provider.Value, result.Value);
            Assert.NotEqual(Guid.Empty, result.ResultId);
        }

        private class TestValueFactory : IValueFactory<string>
        {
            public Task<string> CreateValueAsync(CancellationToken cancellationToken) => Task.FromResult("async value");
        }

        [Fact]
        public async Task Create_WithFactory_CreatesValueAsynchronously()
        {
            var factory = new TestValueFactory();
            var cancellationToken = new CancellationToken();

            var result = await Result<string>.Create(factory, new object(), cancellationToken);

            Assert.Equal("async value", result.Value);
            Assert.NotEqual(Guid.Empty, result.ResultId);
        }
    }
}
