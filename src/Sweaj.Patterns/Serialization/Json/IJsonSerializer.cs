namespace Sweaj.Patterns.Serialization.Json
{
    public interface IJsonSerializer
    {
        T Deserialize<T>(string json);
        Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default);
        string Serialize<T>(T instance);
        Task SerializeAsync<T>(Stream stream, T instance, CancellationToken cancellationToken = default);
    }
}
