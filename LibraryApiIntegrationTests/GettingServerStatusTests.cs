using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApiIntegrationTests
{
    public class GettingServerStatusTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public GettingServerStatusTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateDefaultClient();
        }


        // 1. Do we get a 200?
        [Fact]
        public async Task HasOkStatus()
        {
            var response = await _client.GetAsync("/status");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // 2. Is the response encoded as application/json?
        [Fact]
        public async Task ResponseIsJson()
        {
            var response = await _client.GetAsync("/status");
            var contentType = response.Content.Headers.ContentType.MediaType;

            Assert.Equal("application/json", contentType);
        }

        // 3. Does the data returned look "right"?
        [Fact]
        public async Task HasProperEntity()
        {
            // this is where it is going to get kind of vague and weird.
            var response = await _client.GetAsync("/status");
            var content = await response.Content.ReadAsAsync<GetStatusResponse>();

            Assert.Equal("The Crow Flies at Midnight", content.message);
            Assert.Equal(new DateTime(1969, 4, 20, 23, 59, 00), content.createdAt);
        }

    }


    public class GetStatusResponse
    {
        public string message { get; set; }
        public DateTime createdAt { get; set; }
    }

}
