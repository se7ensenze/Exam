using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace EndToEndTests
{
    [Collection(nameof(StandardTestFixture))]
    public class ListingTests
    {
        private readonly StandardTestFixture _fixture;

        public ListingTests(StandardTestFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task Call_Get_Listing_Success()
        {
           
            var client = _fixture.CreateClient();

            var result =await  client.GetAsync("/api/listing");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Call_Create_Listing_Success()
        {

            var client = _fixture.CreateClient();

            var json = "{\"assetId\":\"3fa85f64-5717-4562-b3fc-2c963f66afa6\",\"price\":10}";
            var content = new StringContent(json, 
                encoding: System.Text.Encoding.UTF8, 
                mediaType: "application/json");

            var result = await client.PostAsync("/api/listing", content);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
    
}