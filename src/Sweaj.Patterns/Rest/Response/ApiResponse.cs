using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Rest.Response
{
    public sealed class ApiResponse<TValue> : Result<TValue>
    {
        protected const string InvalidHttpCodeErrorMessage = "The given http status code is invalid based on the rfc9110 standard.";

        private ApiResponse(Guid correlationId, string message, int httpStatusCode, TValue value)
            : base(Guid.NewGuid(), correlationId, value)
        {
            var trimmedMessage = Guard.Against.NullOrWhiteSpace(message).Trim();

            InternalMessage = trimmedMessage;
            HttpStatusCode = httpStatusCode;
        }
        public bool IsSuccessful => IsSuccessStatusCode(HttpStatusCode);
        public bool IsExceptional => IsServerErrorStatusCode(HttpStatusCode);
        public int HttpStatusCode { get; }

        public bool HasMessage { get => !string.IsNullOrEmpty(InternalMessage); }
        protected string InternalMessage { get; }
        public string ApiMessage => !IsJsonMessage(InternalMessage[0]) ? InternalMessage : string.Empty;
        public string JsonMessage => IsJsonMessage(InternalMessage[0]) ? InternalMessage : string.Empty;

        public static ApiResponse<TValue> From(Guid correlationId, string message, int httpStatusCode, TValue result)
        {
            return new ApiResponse<TValue>(correlationId, message, Guard.Against.AgainstExpression(e => IsHttpStatusCode(e), httpStatusCode, InvalidHttpCodeErrorMessage), result);
        }
        public static ApiResponse<TValue> Ok(Guid correlationId, string message = nameof(Ok), TValue result = default)
        {
            return new ApiResponse<TValue>(correlationId, message, 200, result);
        }

        public static ApiResponse<TValue> Created(Guid correlationId, string message = nameof(Created), TValue result = default)
        {
            return new ApiResponse<TValue>(correlationId, message, 201, result);
        }

        public static ApiResponse<TValue> Accepted(Guid correlationId, string message = nameof(Accepted), TValue result = default)
        {
            return new ApiResponse<TValue>(correlationId, message, 202, result);
        }

        public static ApiResponse<TValue> BadRequest(Guid correlationId, string message = nameof(BadRequest), TValue result = default)
        {
            return new ApiResponse<TValue>(correlationId, message, 400, result);
        }

        public static ApiResponse<TValue> Unauthorized(Guid correlationId, string message = nameof(Unauthorized), TValue result = default)
        {
            return new ApiResponse<TValue>(correlationId, message, 401, result);
        }

        public static ApiResponse<TValue> NotFound(Guid correlationId, string message = nameof(NotFound), TValue result = default)
        {
            return new ApiResponse<TValue>(correlationId, message, 404, result);
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
