namespace Sweaj.Patterns.Serialization.Bytes
{
    public interface IByteSerializer
    {
        object Deserialize(byte[] data);
        T Deserialize<T>(byte[] data);
        byte[] Serialize(object value);
        byte[] Serialize<T>(T value);

    }
}
