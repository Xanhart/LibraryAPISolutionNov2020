using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApiIntegrationTests
{
    public class ResourceSmokeTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ResourceSmokeTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateDefaultClient();
        }

        [Theory]
        [InlineData("status")]
        [InlineData("employees/33")]
        public async Task ResourcesAreAliveAsync(string resource)
        {
            var response = await _client.GetAsync(resource);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
