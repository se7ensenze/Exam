using Example.Domain.Sellers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntregationTests
{
    public class SellerRepositoryTests:IClassFixture<StandardTestFixture>
    {
        private readonly StandardTestFixture _fixture;

        public SellerRepositoryTests(StandardTestFixture fixture) => _fixture = fixture;


        [Fact]
        public async Task Should_Create_Listing_And_Order_Success()
        {
            var repository = _fixture.SellerRepository;

            var seller = new Seller(
                userId: Guid.Parse("BC3E9C2E-B43A-44E7-8FDB-14FA34454C4B"));

            seller.CreateListing(
                assetId: Guid.Parse("430A4854-43A6-4265-BACE-7C62B4B6DCA9"), 
                price: 10_000m);

            await repository.Save(seller);


            var found = await repository.Find(userId: seller.Id, listingId: seller.Listing!.Id);

            Assert.NotNull(found);
            Assert.NotNull(found.Listing);
            Assert.NotNull(found.Listing!.AskOrder);

            Assert.NotSame(seller, found);

        }
    }
}
