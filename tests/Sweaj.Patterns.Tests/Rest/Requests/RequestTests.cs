using Sweaj.Patterns.Rest.Requests;

namespace Sweaj.Patterns.Tests.Rest.Requests
{
    public class RequestTests
    {
        private class TestRequest : Request<string>
        {
            public TestRequest(string value, CancellationToken cancellationToken) : base(value, cancellationToken) { }
        }

        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            var value = "test payload";
            var cancellationToken = new CancellationTokenSource().Token;

            var request = new TestRequest(value, cancellationToken);

            Assert.Equal(value, request.Value);
            Assert.Equal(cancellationToken, request.CancellationToken);
            Assert.NotEqual(Guid.Empty, request.RequestId);
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_WhenValueIsNull()
        {
            var cancellationToken = new CancellationToken();
            Assert.Throws<ArgumentNullException>(() => new TestRequest(null!, cancellationToken));
        }
    }

}
