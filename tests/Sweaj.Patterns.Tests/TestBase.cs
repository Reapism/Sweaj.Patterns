using Microsoft.Extensions.DependencyInjection;
using Sweaj.Patterns.Serialization.Json;
namespace Sweaj.Patterns.Tests
{
    public abstract class TestBase
    {
        protected IServiceProvider ServiceProvider { get; private set; }
        public TestBase()
        {
            IServiceCollection sc = new ServiceCollection();
            ConfigureServices(sc);
            ServiceProvider = sc.BuildServiceProvider();
        }

        protected virtual void ConfigureServices(IServiceCollection s)
        {
            s.AddTransient<IJsonSerializer, JsonSerializerAdapter>();
        }

    }
}
