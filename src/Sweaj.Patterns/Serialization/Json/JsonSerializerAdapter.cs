using System.Text.Json;

namespace Sweaj.Patterns.Serialization.Json
{
    public sealed class JsonSerializerAdapter : IJsonSerializer
    {
        public T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, JsonSerializerOptionsProvider.Web);
        }

        public async Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
        {
            return await JsonSerializer.DeserializeAsync<T>(stream, JsonSerializerOptionsProvider.Web, cancellationToken);
        }

        public string Serialize<T>(T instance)
        {
            return JsonSerializer.Serialize(instance, JsonSerializerOptionsProvider.Web);
        }

        public async Task SerializeAsync<T>(Stream stream, T instance, CancellationToken cancellationToken = default)
        {
            await JsonSerializer.SerializeAsync(stream, instance, JsonSerializerOptionsProvider.Web, cancellationToken);
        }
    }
}
