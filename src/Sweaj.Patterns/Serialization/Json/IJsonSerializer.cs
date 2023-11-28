namespace Sweaj.Patterns.Serialization.Json
{
    public interface IJsonSerializer
    {
        string Serialize<T>(T value);
        T Deserialize<T>(string json);
        ValueTask<Stream> SerializeAsync<TValue>(TValue value);
        ValueTask<TValue> DeserializeAsync<TValue>(Stream value);
    }
}
