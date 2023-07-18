namespace Sweaj.Patterns.Requests
{
    public interface IWebRequest
    {
        Guid RequestId { get; }

    }

    public class WebRequest : IWebRequest
    {
        private WebRequest()
        {
            RequestId = Guid.NewGuid();
        }

        public Guid RequestId { get; }

        public static WebRequest Create()
        {
            return new WebRequest();
        }
    }
}
