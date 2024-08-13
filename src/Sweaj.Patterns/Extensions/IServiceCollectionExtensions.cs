using Microsoft.Extensions.DependencyInjection;

namespace Sweaj.Patterns.Extensions
{
    public static class IServiceCollectionExtensions
    {

        public static IServiceCollection AddAllAs<TBaseType>(this IServiceCollection services, Type assemblyToSearch, ServiceLifetime serviceLifetime)
        {
            // Get all types that implement BaseViewModel and are not abstract
            var types = assemblyToSearch.Assembly
                                        .GetTypes()
                                        .Where(t => typeof(TBaseType).IsAssignableFrom(t) && !t.IsAbstract);

            return serviceLifetime switch
            {
                ServiceLifetime.Singleton => AddAllAsSingleton(services, types),
                ServiceLifetime.Scoped => AddAllAsScoped(services, types),
                ServiceLifetime.Transient => AddAllAsTransient(services, types),
                _ => throw new InvalidOperationException("Service Lifetime is invalid value"),
            };
        }

        private static IServiceCollection AddAllAsSingleton(IServiceCollection services, IEnumerable<Type> types)
        { 
            foreach (var type in types)
            {
                services.AddSingleton(type);
            }
            return services;
        }

        private static IServiceCollection AddAllAsScoped(IServiceCollection services, IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                services.AddScoped(type);
            }
            return services;
        }

        private static IServiceCollection AddAllAsTransient(IServiceCollection services, IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                services.AddTransient(type);
            }
            return services;
        }
    }
}
