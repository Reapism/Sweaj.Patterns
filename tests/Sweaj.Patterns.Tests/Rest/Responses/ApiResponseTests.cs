using Sweaj.Patterns.Rest.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweaj.Patterns.Tests.Rest.Responses
{
    public class ApiResponseTests
    {
        [Fact]
        public void From_ValidInput_CreatesSuccessfulApiResponse()
        {
            var correlationId = Guid.NewGuid();
            var response = ApiResponse<string>.From(correlationId, "payload", "OK", 200);

            Assert.Equal(correlationId, response.CorrelationId);
            Assert.Equal("payload", response.Value);
            Assert.Equal(200, response.HttpStatusCode);
            Assert.True(response.IsSuccessful);
            Assert.True(response.HasMessage);
            Assert.Equal("OK", response.ApiMessage);
            Assert.Empty(response.JsonMessage);
        }

        [Fact]
        public void From_WithJsonMessage_SetsJsonMessageCorrectly()
        {
            var correlationId = Guid.NewGuid();
            var response = ApiResponse<string>.From(correlationId, null, "{\"error\":\"bad request\"}", 400);

            Assert.Equal(400, response.HttpStatusCode);
            Assert.True(ApiResponse<string>.IsClientErrorStatusCode(400));
            Assert.Empty(response.ApiMessage);
            Assert.Equal("{\"error\":\"bad request\"}", response.JsonMessage);
        }

        [Theory]
        [InlineData(99)]
        [InlineData(600)]
        public void From_InvalidHttpStatusCode_Throws(int code)
        {
            var correlationId = Guid.NewGuid();
            Assert.Throws<ArgumentOutOfRangeException>(() => ApiResponse<string>.From(correlationId, "val", "message", code));
        }

        [Fact]
        public void Ok_SetsDefaultsCorrectly()
        {
            var correlationId = Guid.NewGuid();
            var response = ApiResponse<string>.Ok(correlationId);

            Assert.Equal(200, response.HttpStatusCode);
            Assert.Equal("Ok", response.ApiMessage);
        }

        [Theory]
        [InlineData(200, true, false)]
        [InlineData(404, false, false)]
        [InlineData(500, false, true)]
        public void StatusCodePredicates_WorkAsExpected(int code, bool expectedSuccess, bool expectedExceptional)
        {
            Assert.Equal(expectedSuccess, ApiResponse<string>.IsSuccessStatusCode(code));
            Assert.Equal(expectedExceptional, ApiResponse<string>.IsServerErrorStatusCode(code));
        }

        [Theory]
        [InlineData('{', true)]
        [InlineData('[', true)]
        [InlineData('O', false)]
        public void IsJsonMessage_CorrectlyIdentifies(char input, bool expected)
        {
            Assert.Equal(expected, ApiResponse<string>.IsJsonMessage(input));
        }
    }

}
