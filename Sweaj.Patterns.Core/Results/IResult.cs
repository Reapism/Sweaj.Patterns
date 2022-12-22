using Sweaj.Patterns.Requests;

namespace Sweaj.Patterns.Results
{
    public class Result<TRequest, TResult>
    {
        TResult Value { get; }
        Request<TRequest> Request { get; } = new Request<TRequest>().Empty();

        public static Result<TRequest, TResult> From(Request<TRequest> request, TResult result)
        {
            return new Result<TRequest, TResult>();
        }
    }
}
