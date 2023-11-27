using Sweaj.Patterns.Dates;

namespace Sweaj.Patterns.Rest.Requests
{
    /// <summary>
    /// A generic web request with a generic payload of <typeparamref name="TValue"/>.
    /// <para>Refers to the <see cref="WebRequestCollection"/> class for default values.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public abstract class WebRequest<TValue> : Request<TValue>
    {
        public WebRequest(
            [NotNull, ValidatedNotNull] TValue value,
            IDateTimeProvider dateTimeProvider,
            CancellationToken cancellationToken)
            : base(value, dateTimeProvider, cancellationToken)
        {
            Headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// The specific path to request.
        /// </summary>
        public Uri Path { get; set; }

        public HttpMethod Method { get; set; }

        private Dictionary<string, string> Headers { get; }

        /// <summary>
        /// Adds a custom header to this request
        /// </summary>
        /// <param name="headerName"></param>
        /// <param name="headerValue"></param>
        /// <returns></returns>
        public virtual bool AddCustomHeader([ValidatedNotNull, NotNull] string headerName, [ValidatedNotNull, NotNull] string headerValue)
        {
            Guard.Against.NullOrWhiteSpace(headerName);
            Guard.Against.NullOrWhiteSpace(headerValue);

            return Headers.TryAdd(headerName, headerValue);
        }
    }

    public sealed class WebRequestCollection
    {
        private WebRequestCollection()
        {
            DefaultHeaders = new Dictionary<string, string>();
        }

        public static WebRequestCollection Create(Action<WebRequestCollection> configuration)
        {
            var instance = new WebRequestCollection();
            configuration?.Invoke(instance);
            return instance;
        }

        public IReadOnlyDictionary<string, string> DefaultHeaders { get; }
        public Uri BaseUrl { get; set; }

    }
}
