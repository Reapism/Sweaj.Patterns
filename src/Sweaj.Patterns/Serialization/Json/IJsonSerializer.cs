namespace Sweaj.Patterns.Serialization.Json
{
    public interface IJsonSerializer<TValue> : ISerializer<string, TValue>
    {
        ValueTask<Stream> SerializeAsync(TValue value);
        ValueTask<TValue> DeserializeAsync(Stream value);
    }

    public interface IByteSerializer<TValue> : ISerializer<TValue, byte[]>
    { }
}
