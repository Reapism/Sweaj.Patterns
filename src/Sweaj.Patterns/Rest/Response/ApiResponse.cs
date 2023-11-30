using Sweaj.Patterns.Data.Values;

namespace Sweaj.Patterns.Rest.Response
{
    public sealed class ApiResponse<TValue> : Result<TValue>
    {
        protected const string InvalidHttpCodeErrorMessage = "The given http status code is invalid based on the rfc9110 standard.";
        protected const string OkMessage = "OK";
        protected const string CreatedMessage = "CREATED";
        protected const string AcceptedMessage = "ACCEPTED";
        protected const string BadRequestMessage = "BAD REQUEST";
        protected const string UnauthorizedMessage = "UNAUTHORIZED";
        protected const string NotFoundMessage = "NOT FOUND";

        private ApiResponse(Guid correlationId, TValue value, string message, int httpStatusCode)
            : base(correlationId, value)
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

        public static ApiResponse<TValue> From(Guid correlationId, TValue value, string message, int httpStatusCode)
        {
            return new ApiResponse<TValue>(correlationId, value, message, Guard.Against.AgainstExpression(e => IsHttpStatusCode(e), httpStatusCode, InvalidHttpCodeErrorMessage));
        }
        public static ApiResponse<TValue> Ok(Guid correlationId, TValue value = default, string message = OkMessage)
        {
            return new ApiResponse<TValue>(correlationId, value, message, 200);
        }

        public static ApiResponse<TValue> Created(Guid correlationId, TValue value = default, string message = CreatedMessage)
        {
            return new ApiResponse<TValue>(correlationId, value, message, 201);
        }

        public static ApiResponse<TValue> Accepted(Guid correlationId, TValue value = default, string message = AcceptedMessage)
        {
            return new ApiResponse<TValue>(correlationId, value, message, 202);
        }

        public static ApiResponse<TValue> BadRequest(Guid correlationId, TValue value = default, string message = BadRequestMessage)
        {
            return new ApiResponse<TValue>(correlationId, value, message, 400);
        }

        public static ApiResponse<TValue> Unauthorized(Guid correlationId, TValue value = default, string message = UnauthorizedMessage)
        {
            return new ApiResponse<TValue>(correlationId, value, message, 401);
        }

        public static ApiResponse<TValue> NotFound(Guid correlationId, TValue value = default, string message = NotFoundMessage)
        {
            return new ApiResponse<TValue>(correlationId, value, message, 404);
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
