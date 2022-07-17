using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EndToEndTests
{
    [Collection(nameof(StandardTestFixture))]
    public class OfferTests
    {
        private readonly StandardTestFixture _fixture;

        public OfferTests(StandardTestFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task Call_Get_Offer_Success()
        { 
            var client = _fixture.CreateClient();

            var listingId = Guid.Parse("0B85EA19-85E1-459D-8317-71BAB9487190");

            var result = await client.GetAsync($"/api/offer/{listingId}");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        }
    }

}
