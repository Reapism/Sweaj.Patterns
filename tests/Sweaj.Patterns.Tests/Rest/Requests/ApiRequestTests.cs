using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweaj.Patterns.Tests.Rest.Requests
{
    using Sweaj.Patterns.Rest.Requests;
    using System;
    using System.Threading;
    using Xunit;

    public class ApiRequestTests
    {


        private class TestApiRequest : ApiRequest<string>
        {
            public TestApiRequest(Guid correlationId, string value, CancellationToken cancellationToken)
                : base(correlationId, value, cancellationToken) { }

            public TestApiRequest(string value, CancellationToken cancellationToken)
                : base(value, cancellationToken) { }
        }

        [Fact]
        public void ApiRequest_WithCorrelationId_SetsPropertiesCorrectly()
        {
            var correlationId = Guid.NewGuid();
            var value = "api payload";
            var token = new CancellationTokenSource().Token;

            var request = new TestApiRequest(correlationId, value, token);

            Assert.Equal(value, request.Value);
            Assert.Equal(token, request.CancellationToken);
            Assert.Equal(correlationId, request.CorrelationId);
        }

        [Fact]
        public void ApiRequest_WithoutCorrelationId_GeneratesCorrelationId()
        {
            var value = "api payload";
            var token = new CancellationTokenSource().Token;

            var request = new TestApiRequest(value, token);

            Assert.Equal(value, request.Value);
            Assert.Equal(token, request.CancellationToken);
            Assert.NotEqual(Guid.Empty, request.CorrelationId);
        }
    }

}
