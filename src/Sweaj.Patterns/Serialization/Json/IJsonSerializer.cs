using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Serialization.Json
{

    /// <summary>
    /// Defines methods for serializing and deserializing JSON data.
    /// </summary>
    [Trackable]
    public interface IJsonSerializer
    {
        /// <summary>
        /// Serializes the specified value to a JSON string.
        /// </summary>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        /// <param name="value">The value to serialize.</param>
        /// <returns>A JSON string representation of the <paramref name="value"/>.</returns>
        string Serialize<T>(T value);

        /// <summary>
        /// Deserializes the specified JSON string to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The target type to deserialize the JSON into.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>
        /// An object of type <typeparamref name="T"/> if deserialization is successful; otherwise, <c>null</c>.
        /// </returns>
        T? Deserialize<T>(string json);

        /// <summary>
        /// Asynchronously serializes the specified value to a JSON <see cref="Stream"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
        /// <param name="value">The value to serialize.</param>
        /// <returns>
        /// A <see cref="ValueTask{TResult}"/> that represents the asynchronous operation,
        /// containing a <see cref="Stream"/> with the JSON representation of <paramref name="value"/>.
        /// </returns>
        ValueTask<Stream> SerializeAsync<TValue>(TValue value);

        /// <summary>
        /// Asynchronously deserializes the specified JSON <see cref="Stream"/> to an object of type <typeparamref name="TValue"/>.
        /// </summary>
        /// <typeparam name="TValue">The target type to deserialize the JSON into.</typeparam>
        /// <param name="value">The <see cref="Stream"/> containing the JSON data.</param>
        /// <returns>
        /// A <see cref="ValueTask{TResult}"/> that represents the asynchronous operation,
        /// containing an object of type <typeparamref name="TValue"/> if deserialization is successful; otherwise, <c>null</c>.
        /// </returns>
        ValueTask<TValue?> DeserializeAsync<TValue>(Stream value);
    }

}
