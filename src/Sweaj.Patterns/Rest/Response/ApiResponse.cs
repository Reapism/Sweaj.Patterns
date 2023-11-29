using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Rest.Response
{
    public sealed class ApiResponse : IResult<Guid>
    {
        protected const string InvalidHttpCodeErrorMessage = "The given http status code is invalid based on the rfc9110 standard.";

        protected ApiResponse(Guid correlationId, string message, int httpStatusCode)
        {
            var trimmedMessage = Guard.Against.NullOrWhiteSpace(message).Trim();

            ResultId = Guid.NewGuid();
            InternalMessage = trimmedMessage;
            HttpStatusCode = httpStatusCode;
            CorrelationId = correlationId;
        }

        public bool IsSuccessful => IsSuccessStatusCode(HttpStatusCode);
        public bool IsExceptional => IsServerErrorStatusCode(HttpStatusCode);
        public int HttpStatusCode { get; }

        public bool HasMessage { get => !string.IsNullOrEmpty(InternalMessage); }
        protected string InternalMessage { get; }
        public string ApiMessage => !IsJsonMessage(InternalMessage[0]) ? InternalMessage : string.Empty;
        public string JsonMessage => IsJsonMessage(InternalMessage[0]) ? InternalMessage : string.Empty;

        public Guid ResultId { get; }

        public Guid CorrelationId { get; }

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
            return firstInternalMessageCharacter is '{' or '[';
        }

        public static ApiResponse From(Guid correlationId, string message, int httpStatusCode)
        {
            return new ApiResponse(correlationId, message, Guard.Against.AgainstExpression(e => IsHttpStatusCode(e), httpStatusCode, InvalidHttpCodeErrorMessage));
        }

        public static ApiResponse Ok(Guid correlationId, string message = nameof(Ok))
        {
            return new ApiResponse(correlationId, message, 200);
        }

        public static ApiResponse Created(Guid correlationId, string message = nameof(Created))
        {
            return new ApiResponse(correlationId, message, 201);
        }

        public static ApiResponse Accepted(Guid correlationId, string message = nameof(Accepted))
        {
            return new ApiResponse(correlationId, message, 202);
        }

        public static ApiResponse BadRequest(Guid correlationId, string message = nameof(BadRequest))
        {
            return new ApiResponse(correlationId, message, 400);
        }

        public static ApiResponse Unauthorized(Guid correlationId, string message = nameof(Unauthorized))
        {
            return new ApiResponse(correlationId, message, 403);
        }

        public static ApiResponse NotFound(Guid correlationId, string message = nameof(NotFound))
        {
            return new ApiResponse(correlationId, message, 404);
        }

        public static ApiResponse ServerExceptional(Guid correlationId, string message = "The server has encountered a situation it does not know how to handle.")
        {
            return new ApiResponse(correlationId, message, 500);
        }
    }

    public sealed class ApiResponse<T> : Result<Guid, T>
        where T : IValueProvider<T>
    {
        protected const string InvalidHttpCodeErrorMessage = "The given http status code is invalid based on the rfc9110 standard.";

        private ApiResponse(Guid correlationId, string message, int httpStatusCode, T value)
            : base(Guid.NewGuid(), correlationId, value)
        {
            Value = Guard.Against.Null(value);
            
            var trimmedMessage = Guard.Against.NullOrWhiteSpace(message).Trim();

            InternalMessage = trimmedMessage;
            HttpStatusCode = httpStatusCode;
        }

        public T Value { get; }
        public bool IsSuccessful => IsSuccessStatusCode(HttpStatusCode);
        public bool IsExceptional => IsServerErrorStatusCode(HttpStatusCode);
        public int HttpStatusCode { get; }

        public bool HasMessage { get => !string.IsNullOrEmpty(InternalMessage); }
        protected string InternalMessage { get; }
        public string ApiMessage => !IsJsonMessage(InternalMessage[0]) ? InternalMessage : string.Empty;
        public string JsonMessage => IsJsonMessage(InternalMessage[0]) ? InternalMessage : string.Empty;

        public static ApiResponse<T> From(Guid correlationId, string message, int httpStatusCode, T result)
        {
            return new ApiResponse<T>(correlationId, message, Guard.Against.AgainstExpression(e => IsHttpStatusCode(e), httpStatusCode, InvalidHttpCodeErrorMessage), result);
        }
        public static ApiResponse<T> Ok(Guid correlationId, string message = nameof(Ok), T result = default)
        {
            return new ApiResponse<T>(correlationId, message, 200, result);
        }

        public static ApiResponse<T> Created(Guid correlationId, string message = nameof(Created), T result = default)
        {
            return new ApiResponse<T>(correlationId, message, 201, result);
        }

        public static ApiResponse<T> Accepted(Guid correlationId, string message = nameof(Accepted), T result = default)
        {
            return new ApiResponse<T>(correlationId, message, 202, result);
        }

        public static ApiResponse<T> BadRequest(Guid correlationId, string message = nameof(BadRequest), T result = default)
        {
            return new ApiResponse<T>(correlationId, message, 400, result);
        }

        public static ApiResponse<T> Unauthorized(Guid correlationId, string message = nameof(Unauthorized), T result = default)
        {
            return new ApiResponse<T>(correlationId, message, 401, result);
        }

        public static ApiResponse<T> NotFound(Guid correlationId, string message = nameof(NotFound), T result = default)
        {
            return new ApiResponse<T>(correlationId, message, 404, result);
        }

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
            return firstInternalMessageCharacter is '{' or '[';
        }
    }
}
