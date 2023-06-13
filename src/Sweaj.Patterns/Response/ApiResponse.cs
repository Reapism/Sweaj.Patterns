using Sweaj.Patterns.Response;

namespace Sweaj.Patterns.Responses
{
    public class ApiResponse : IResult<Guid>
    {
        protected const string InvalidHttpCodeErrorMessage = "The given http status code is invalid based on the rfc9110 standard.";

        protected ApiResponse(string message, int httpStatusCode)
        {
            var trimmedMessage = Guard.Against.NullOrWhiteSpace(message).Trim();

            ResultId = Guid.NewGuid();
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

        public Guid ResultId { get; }

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

        public static ApiResponse From(string message, int httpStatusCode)
        {
            return new ApiResponse(message, Guard.Against.AgainstExpression<int>(e => IsHttpStatusCode(e), httpStatusCode, InvalidHttpCodeErrorMessage));
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
        private ApiResponse(string message, int httpStatusCode, T result)
            : base(message, httpStatusCode)
        {
            Result = Guard.Against.Null(result); ;
        }

        public T Result { get; }

        public static ApiResponse<T> From(string message, int httpStatusCode, T result)
        {
            return new ApiResponse<T>(message, Guard.Against.AgainstExpression<int>(e => IsHttpStatusCode(e), httpStatusCode, InvalidHttpCodeErrorMessage), result);
        }
        public static ApiResponse<T> Ok(string message = nameof(Ok), T result = default)
        {
            return new ApiResponse<T>(message, 200, result);
        }

        public static ApiResponse<T> Created(string message = nameof(Created), T result = default)
        {
            return new ApiResponse<T>(message, 201, result);
        }

        public static ApiResponse<T> Accepted(string message = nameof(Accepted), T result = default)
        {
            return new ApiResponse<T>(message, 202, result);
        }

        public static ApiResponse<T> BadRequest( string message = nameof(BadRequest), T result = default)
        {
            return new ApiResponse<T>(message, 400, result);
        }

        public static ApiResponse<T> Unauthorized(string message = nameof(Unauthorized), T result = default)
        {
            return new ApiResponse<T>(message, 401, result);
        }

        public static ApiResponse<T> NotFound(string message = nameof(NotFound), T result = default)
        {
            return new ApiResponse<T>(message, 404, result);
        }
    }
}
