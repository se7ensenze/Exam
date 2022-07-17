using Example.Domain.Sellers;
using Example.Domain.ValueObjects;

namespace DomainTests.Sellers
{
    public class SellerTests
    {
        [Fact]
        public void Create_Listing_Success()
        {
            //arrange
            var seller = new Seller(
                userId: Guid.Parse("BC7F3FC6-4A5A-4E78-8A81-D278266F2FC7"));

            var assetId = Guid.Parse("7388200D-E4D5-4C5C-BE8E-14D453CA7166");
            var price = 10_000m;

            //action

            Assert.Null(seller.Listing);

            seller.CreateListing(assetId, price);

            //assert
            Assert.NotNull(seller.Listing);

            var domainEvents = seller.DomainEvents;

            Assert.NotNull(domainEvents);
            Assert.Equal(expected: 1, actual: domainEvents?.Count);

            if (domainEvents != null)
            {
                var eventData = domainEvents.First() as ListingCreatedEventArgs;

                Assert.NotNull(eventData);
                Assert.Equal(price, eventData.Price);
                Assert.Equal(assetId, eventData.AssetId);
                Assert.Equal(seller.Id, eventData.UserId);
                Assert.NotEqual(Guid.Empty, eventData.ListingId);
                Assert.NotEqual(Guid.Empty, eventData.AskOrderId);
            }

            var createdListing = seller.Listing;

            Assert.NotNull(createdListing);
            Assert.NotNull(createdListing.AskOrder);
            Assert.NotNull(createdListing.Offers);

            Assert.NotEqual(Guid.Empty, createdListing.Id);

            var createdOrder = createdListing.AskOrder;

            Assert.NotEqual(Guid.Empty, createdOrder.Id);
            Assert.Equal(price, createdOrder.Price);
            Assert.Equal(assetId, createdOrder.AssetId);
            Assert.Equal(OrderSide.Ask.Name, createdOrder.Side.Name);
            Assert.Equal(OrderStatus.Open.Name, createdOrder.Status.Name);
        }

        [Fact]
        public void Cancel_Listing_With_No_Offer_Success()
        {
            //arrange
            var assetId = Guid.Parse("7388200D-E4D5-4C5C-BE8E-14D453CA7166");
            var price = 10_000m;

            var tarGetListingId = Guid.Parse("5FC8427A-BEA9-4B13-A655-19EB34BF9C68");

            var order = new Order(
                side: OrderSide.Ask,
                assetId: assetId,
                price: price);

            var listing = new Listing(
                id: tarGetListingId,
                askOrder: order,
                offerCollection: new OfferCollection());

            var seller = new Seller(
                userId: Guid.Parse("BC7F3FC6-4A5A-4E78-8A81-D278266F2FC7"),
                listing: listing);


            //action
            seller.CancelListing();

            //assert
            Assert.NotNull(seller.Listing);

            var domainEvents = seller.DomainEvents;

            Assert.NotNull(domainEvents);
            Assert.Equal(expected: 1, actual: domainEvents?.Count);

            if (domainEvents != null)
            {
                var eventData = domainEvents.First() as ListingCancelledEventArgs;

                Assert.NotNull(eventData);
                Assert.Equal(price, eventData.Price);
                Assert.Equal(assetId, eventData.AssetId);
                Assert.Equal(seller.Id, eventData.UserId);
                Assert.Single(eventData.CancelledOrders);
            }

            var calncelledListing = seller.Listing;

            Assert.NotNull(calncelledListing);
            Assert.NotNull(calncelledListing.AskOrder);
            Assert.NotNull(calncelledListing.Offers);

            Assert.Equal(tarGetListingId, calncelledListing.Id);

            var createdOrder = calncelledListing.AskOrder;

            Assert.NotEqual(Guid.Empty, createdOrder.Id);
            Assert.Equal(price, createdOrder.Price);
            Assert.Equal(assetId, createdOrder.AssetId);
            Assert.Equal(OrderSide.Ask.Name, createdOrder.Side.Name);
            Assert.Equal(OrderStatus.Cancelled.Name, createdOrder.Status.Name);
        }


        [Fact]
        public void Cancel_Listing_With_Two_Offers_Success()
        {
            //arrange
            var assetId = Guid.Parse("A7DB9147-4369-4355-BFD5-8E76FFD8769E");
            var price = 10_000m;

            var tarGetListingId = Guid.Parse("A848CD83-C3AF-4AEF-91F3-91966756838E");

            var order = new Order(
                side: OrderSide.Ask,
                assetId: assetId,
                price: price);

            var offers = new OfferCollection(new Offer[] {
                new Offer(
                    id: Guid.Parse("CAC8136C-2773-4E9C-BCC9-6EDF603244C0"),
                    bidOrder: new Order(
                        id: Guid.Parse("4AC90188-5509-4CD2-8BCD-39DEACE2F7D8"),
                        side: OrderSide.Bid,
                        assetId: assetId,
                        price: 9_900m,
                        status: OrderStatus.Open)),

                new Offer(
                    id: Guid.Parse("B3DD6ED2-2B7D-404D-BBF0-22084E8E9EB7"),
                    bidOrder: new Order(
                        id: Guid.Parse("FB8D2AE0-C105-4739-820D-7383558A0E88"),
                        side: OrderSide.Bid,
                        assetId: assetId,
                        price: 9_500m,
                        status: OrderStatus.Open))
            });

            var listing = new Listing(
                id: tarGetListingId,
                askOrder: order,
                offerCollection: offers);

            var seller = new Seller(
                userId: Guid.Parse("B941C78F-96CC-4044-8F23-138150B34B54"),
                listing: listing);


            //action
            seller.CancelListing();

            //assert
            Assert.NotNull(seller.Listing);

            var domainEvents = seller.DomainEvents;

            Assert.NotNull(domainEvents);
            Assert.Equal(expected: 1, actual: domainEvents?.Count);

            if (domainEvents != null)
            {
                var eventData = domainEvents.First() as ListingCancelledEventArgs;

                Assert.NotNull(eventData);
                Assert.Equal(price, eventData.Price);
                Assert.Equal(assetId, eventData.AssetId);
                Assert.Equal(seller.Id, eventData.UserId);
                Assert.Equal(3, eventData.CancelledOrders.Length);
            }

            var calncelledListing = seller.Listing;

            Assert.NotNull(calncelledListing);
            Assert.NotNull(calncelledListing.AskOrder);
            Assert.NotNull(calncelledListing.Offers);

            Assert.Equal(tarGetListingId, calncelledListing.Id);

            var createdOrder = calncelledListing.AskOrder;

            Assert.NotEqual(Guid.Empty, createdOrder.Id);
            Assert.Equal(price, createdOrder.Price);
            Assert.Equal(assetId, createdOrder.AssetId);
            Assert.Equal(OrderSide.Ask.Name, createdOrder.Side.Name);
            Assert.Equal(OrderStatus.Cancelled.Name, createdOrder.Status.Name);

            calncelledListing.Offers
                .AsReadOnly
                .ToList()
                .ForEach(offer =>
                {
                    Assert.Equal(OrderStatus.Cancelled.Name, offer.BidOrder.Status.Name);
                });
        }
    }
}