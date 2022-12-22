namespace Sweaj.Patterns.Results
{
    public class ApiResponse
    {
        protected ApiResponse(string message, int httpStatusCode)
        {
            Guard.Against.NullOrWhiteSpace(message);

            var trimmedMessage = message.Trim();

            Guard.Against.Zero(trimmedMessage.Length);

            InternalMessage = trimmedMessage;
            HttpStatusCode = httpStatusCode;

            IsSuccessful = IsSuccessStatusCode(httpStatusCode);
            IsExceptional = IsServerErrorStatusCode(httpStatusCode);
        }

        public bool IsSuccessful { get; }
        public bool IsExceptional { get; }
        public int HttpStatusCode { get; }

        public bool HasMessage { get => !string.IsNullOrEmpty(InternalMessage); }
        protected string InternalMessage { get; }
        public string ApiMessage => !IsJsonMessage(InternalMessage[0]) ? InternalMessage : string.Empty;
        public string JsonMessage => IsJsonMessage(InternalMessage[0]) ? InternalMessage : string.Empty;

        /// <summary>
        /// Returns whether the <paramref name="httpStatusCode"/> is a valid HTTP status code using rfc9110 standard.
        /// <para>see <see cref="https://httpwg.org/specs/rfc9110.html#overview.of.status.codes"/></para>
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <returns>Returns whether the <paramref name="httpStatusCode"/> is in range of valid HTTP status codes.</returns>
        public static bool IsHttpStatusCode(int httpStatusCode)
        {
            return httpStatusCode is >= 100 and <= 599;
        }

        public static bool IsSuccessStatusCode(int httpStatusCode)
        {
            return httpStatusCode is >= 200 and <= 299;
        }

        public static bool IsClientErrorStatusCode(int httpStatusCode)
        {
            return httpStatusCode is >= 400 and <= 499;
        }

        public static bool IsServerErrorStatusCode(int httpStatusCode)
        {
            return httpStatusCode is >= 500 and <= 599;
        }

        public static bool IsJsonMessage(char firstInternalMessageCharacter)
        {
            return firstInternalMessageCharacter == '{' || firstInternalMessageCharacter == '[';
        }

        public static ApiResponse Ok(string message = nameof(Ok))
        {
            return new ApiResponse(message, 200);
        }

        public static ApiResponse Created(string message = nameof(Created))
        {
            return new ApiResponse(message, 201);
        }

        public static ApiResponse Accepted(string message = nameof(Accepted))
        {
            return new ApiResponse(message, 202);
        }

        public static ApiResponse BadRequest(string message = nameof(BadRequest))
        {
            return new ApiResponse(message, 400);
        }

        public static ApiResponse Unauthorized(string message = nameof(Unauthorized))
        {
            return new ApiResponse(message, 403);
        }

        public static ApiResponse NotFound(string message = nameof(NotFound))
        {
            return new ApiResponse(message, 404);
        }

        public static ApiResponse ServerExceptional(string message = "The server has encountered a situation it does not know how to handle.")
        {
            return new ApiResponse(message, 500);
        }
    }

    public sealed class ApiResponse<T> : ApiResponse
    {
        private ApiResponse(string message, int httpStatusCode, T value)
            : base(message, httpStatusCode)
        {
            Guard.Against.Null(value);
            Value = value;
        }

        public T Value { get; }

        public static ApiResponse<T> Ok(T value, string message = nameof(Ok))
        {
            return new ApiResponse<T>(message, 200, value);
        }
    }
}
