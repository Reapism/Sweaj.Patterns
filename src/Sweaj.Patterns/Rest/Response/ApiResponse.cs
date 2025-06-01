using Sweaj.Patterns.Attributes;

namespace Sweaj.Patterns.Rest.Response
{
    /// <summary>
    /// Represents a structured API response that includes a result value, HTTP status code, correlation ID, and messages.
    /// </summary>
    /// <typeparam name="TValue">The type of the response payload.</typeparam>
    [Trackable]
    public class ApiResponse<TValue> : Result<TValue?>, ICorrelationIdProvider<Guid>
    {
        /// <summary>
        /// Default error message used when an HTTP status code is invalid according to RFC 9110.
        /// </summary>
        public const string InvalidHttpCodeErrorMessage = "The given http status code is invalid based on the rfc9110 standard.";

        /// <summary>
        /// Default message used when the status code is 200 (OK).
        /// </summary>
        protected const string OkMessage = nameof(Ok);

        private ApiResponse(in Guid correlationId, TValue value, string message, int httpStatusCode)
            : base(value)
        {
            var trimmedMessage = Guard.Against.NullOrWhiteSpace(message).Trim();
            CorrelationId = correlationId;
            InternalMessage = trimmedMessage;
            HttpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Gets the correlation ID associated with this response.
        /// </summary>
        public Guid CorrelationId { get; }

        /// <summary>
        /// Gets a value indicating whether the HTTP status code indicates a successful response.
        /// </summary>
        public bool IsSuccessful => IsSuccessStatusCode(HttpStatusCode);

        /// <summary>
        /// Gets a value indicating whether the HTTP status code indicates a server-side error.
        /// </summary>
        public bool IsExceptional => IsServerErrorStatusCode(HttpStatusCode);

        /// <summary>
        /// Gets the HTTP status code associated with the response.
        /// </summary>
        public int HttpStatusCode { get; }

        /// <summary>
        /// Gets a value indicating whether a non-empty internal message exists.
        /// </summary>
        public bool HasMessage => !string.IsNullOrEmpty(InternalMessage);

        /// <summary>
        /// Gets the raw internal message.
        /// </summary>
        protected string InternalMessage { get; }

        /// <summary>
        /// Gets the user-facing API message if it is not a JSON object.
        /// </summary>
        public string ApiMessage => !IsJsonMessage(InternalMessage[0]) ? InternalMessage : string.Empty;

        /// <summary>
        /// Gets the internal JSON message if present.
        /// </summary>
        public string JsonMessage => IsJsonMessage(InternalMessage[0]) ? InternalMessage : string.Empty;

        /// <summary>
        /// Creates a new <see cref="ApiResponse{TValue}"/> from the provided values.
        /// </summary>
        /// <param name="correlationId">The correlation ID for the response.</param>
        /// <param name="value">The payload value.</param>
        /// <param name="message">The message string (can be plain text or JSON).</param>
        /// <param name="httpStatusCode">The HTTP status code (must be 100–599).</param>
        /// <returns>An <see cref="ApiResponse{TValue}"/> instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the status code is not valid per RFC 9110.</exception>
        public static ApiResponse<TValue> From(Guid correlationId, TValue? value, string message, int httpStatusCode)
        {
            if (httpStatusCode == 200)
            {
                return Ok(correlationId, value, message);
            }
            return new ApiResponse<TValue>(
                correlationId,
                value,
                message,
                Guard.Against.Expression(e => !IsHttpStatusCode(e), httpStatusCode, InvalidHttpCodeErrorMessage, exceptionCreator: () => throw new ArgumentOutOfRangeException(InvalidHttpCodeErrorMessage))
            );
        }

        /// <summary>
        /// Creates a 200 OK <see cref="ApiResponse{TValue}"/> instance.
        /// </summary>
        /// <param name="correlationId">The correlation ID for tracking the response.</param>
        /// <param name="value">The optional response value.</param>
        /// <param name="message">The message to include. Defaults to \"Ok\".</param>
        /// <returns>An <see cref="ApiResponse{TValue}"/> with status code 200.</returns>
        public static ApiResponse<TValue> Ok(Guid correlationId, TValue value = default, string message = OkMessage)
        {
            return new ApiResponse<TValue>(correlationId, value, message, 200);
        }

        /// <summary>
        /// Determines whether the given status code is within the valid HTTP status code range (100–599).
        /// </summary>
        public static bool IsHttpStatusCode(int httpStatusCode) =>
            httpStatusCode is >= 100 and <= 599;

        /// <summary>
        /// Determines whether the status code represents a successful response (200–299).
        /// </summary>
        public static bool IsSuccessStatusCode(int httpStatusCode) =>
            httpStatusCode is >= 200 and <= 299;

        /// <summary>
        /// Determines whether the status code represents a client error (400–499).
        /// </summary>
        public static bool IsClientErrorStatusCode(int httpStatusCode) =>
            httpStatusCode is >= 400 and <= 499;

        /// <summary>
        /// Determines whether the status code represents a server error (500–599).
        /// </summary>
        public static bool IsServerErrorStatusCode(int httpStatusCode) =>
            httpStatusCode is >= 500 and <= 599;

        /// <summary>
        /// Determines whether the provided character indicates the start of a JSON message.
        /// </summary>
        /// <param name="firstInternalMessageCharacter">The first character of the internal message.</param>
        /// <returns><c>true</c> if the character indicates JSON; otherwise, <c>false</c>.</returns>
        public static bool IsJsonMessage(char firstInternalMessageCharacter) =>
            firstInternalMessageCharacter is '{' or '[';
    }
}
