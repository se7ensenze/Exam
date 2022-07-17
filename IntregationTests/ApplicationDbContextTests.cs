using Example.Infrastructure.Entities;
using Example.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace IntregationTests
{
    public class ApplicationDbContextTests :IClassFixture<StandardTestFixture>
    {
        private readonly StandardTestFixture _fixture;

        public ApplicationDbContextTests(StandardTestFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task Add_Listing_Should_Success()
        {
            var listing = new Listing() { 
                ListingId = Guid.Parse("A219B624-CB22-48C9-9FC8-31D27975CD57"),
                AssetId = Guid.Parse("65DA15C3-1528-4E46-B1BB-2B3D72043B40"),
                CreatedTimestamp = DateTime.Now,
                OrderId = Guid.Parse("E6C81391-A130-489F-9939-5026464F55EE"),
                UserId = Guid.Parse("FCBE9818-F34F-4249-A4C4-AAB5D56BD5BF"),
            };

            var dbContext = _fixture.DbContext;
            
            dbContext.Listings.Add(listing);

            await dbContext.SaveChangesAsync();

            Assert.Equal(1, dbContext.Listings.Count(e => e.ListingId == listing.ListingId));
        }
    }
}