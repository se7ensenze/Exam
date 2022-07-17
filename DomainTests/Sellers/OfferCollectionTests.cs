using Example.Domain.Sellers;
using Example.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTests.Sellers
{
    public class OfferCollectionTests
    {

        [Fact]
        public void Create_Collection_With_Two_Offers_Correctly()
        {

            var assetId = Guid.Parse("BF7E8462-0164-4435-8864-59BBCEB8DF3D");

            var firstOffer = new Offer(
                id: Guid.Parse("55229573-EDB6-4452-895F-37E816AC9D3D"),
                bidOrder: new Order(
                    id: Guid.Parse("86BBC8D8-6A68-4D9D-AB2F-0D23913D1ACF"),
                    side: OrderSide.Bid,
                    assetId: assetId,
                    price: 10_000m,
                    status: OrderStatus.Open));

            var secondOffer = new Offer(
               id: Guid.Parse("851598DE-0E09-45DE-A825-4691411F10B4"),
               bidOrder: new Order(
                   id: Guid.Parse("479CC642-DA11-4E9D-8A37-83A96A085A89"),
                   side: OrderSide.Bid,
                   assetId: assetId,
                   price: 10_000m,
                   status: OrderStatus.Open));

            OfferCollection offerCollection = new(offers: new Offer[] {
                firstOffer, secondOffer
            });

            Assert.Equal(2, offerCollection.AsReadOnly.Count);
        }

        [Fact]
        public void Collection_Sould_Return_Specific_Offer_Correctly()
        {

            var assetId = Guid.Parse("ED456DA9-4B51-475F-939D-234A0E02B3CD");
            var targetOfferId = Guid.Parse("235FC48D-F8F0-40CD-9A1C-A85DED4A94A3");

            var targetOffer = new Offer(
                id: targetOfferId,
                bidOrder: new Order(
                    id: Guid.Parse("81454D40-2325-4790-B027-DD87DD65139E"),
                    side: OrderSide.Bid,
                    assetId: assetId,
                    price: 10_000m,
                    status: OrderStatus.Open));

            var dummyOffer = new Offer(
               id: Guid.Parse("299C2A3E-B4AD-4F5A-A73D-1B3A17AE574E"),
               bidOrder: new Order(
                   id: Guid.Parse("E30A491C-136E-410A-B6B6-BBC7B9B051F0"),
                   side: OrderSide.Bid,
                   assetId: assetId,
                   price: 10_000m,
                   status: OrderStatus.Open));

            OfferCollection offerCollection = new(offers: new Offer[] { 
                dummyOffer, targetOffer
            });


            var foundOffer = offerCollection.Get(targetOfferId);

            Assert.NotNull(foundOffer);

            Assert.True(object.ReferenceEquals(foundOffer, targetOffer));
            Assert.Equal(targetOfferId, foundOffer.Id);

        }

        [Fact]
        public void Collection_Sould_Throw_Exception_When_Not_Found()
        {

            var assetId = Guid.Parse("ED456DA9-4B51-475F-939D-234A0E02B3CD");

            var targetOffer = new Offer(
                id: Guid.Parse("235FC48D-F8F0-40CD-9A1C-A85DED4A94A3"),
                bidOrder: new Order(
                    id: Guid.Parse("81454D40-2325-4790-B027-DD87DD65139E"),
                    side: OrderSide.Bid,
                    assetId: assetId,
                    price: 10_000m,
                    status: OrderStatus.Open));

            var dummyOffer = new Offer(
               id: Guid.Parse("299C2A3E-B4AD-4F5A-A73D-1B3A17AE574E"),
               bidOrder: new Order(
                   id: Guid.Parse("E30A491C-136E-410A-B6B6-BBC7B9B051F0"),
                   side: OrderSide.Bid,
                   assetId: assetId,
                   price: 10_000m,
                   status: OrderStatus.Open));

            OfferCollection offerCollection = new(offers: new Offer[] {
                dummyOffer, targetOffer
            });


            Assert.Throws<OfferNotFoundException>(() => offerCollection.Get(Guid.Empty));

        }

        [Fact]
        public void All_Offer_Sould_Be_Cancelled()
        {

            var assetId = Guid.Parse("7A3310A7-1A10-4D40-95D5-E2A6D6185EB2");

            var firstOffer = new Offer(
                id: Guid.Parse("C0A79252-63A7-4F70-AA45-0CD8A5B1F330"),
                bidOrder: new Order(
                    id: Guid.Parse("81454D40-2325-4790-B027-DD87DD65139E"),
                    side: OrderSide.Bid,
                    assetId: assetId,
                    price: 10_000m,
                    status: OrderStatus.Open));

            var secondOffer = new Offer(
               id: Guid.Parse("299C2A3E-B4AD-4F5A-A73D-1B3A17AE574E"),
               bidOrder: new Order(
                   id: Guid.Parse("E30A491C-136E-410A-B6B6-BBC7B9B051F0"),
                   side: OrderSide.Bid,
                   assetId: assetId,
                   price: 10_000m,
                   status: OrderStatus.Open));

            OfferCollection offerCollection = new(offers: new Offer[] {
                firstOffer, secondOffer
            });


            offerCollection.Cancel();


            Assert.Equal(OrderStatus.Cancelled.Name, firstOffer.BidOrder.Status.Name);
            Assert.Equal(OrderStatus.Cancelled.Name, secondOffer.BidOrder.Status.Name);
        }

        [Fact]
        public void All_Offer_Sould_Be_Closed()
        {

            var assetId = Guid.Parse("8BA8260D-EF2A-440F-B708-21C0BBADB6D7");

            var firstOffer = new Offer(
                id: Guid.Parse("FB151B49-B3A7-46D2-B4D5-640F9A74C9EA"),
                bidOrder: new Order(
                    id: Guid.Parse("35C6F888-898B-48A3-8766-8537A6A76AAE"),
                    side: OrderSide.Bid,
                    assetId: assetId,
                    price: 10_000m,
                    status: OrderStatus.Open));

            var secondOffer = new Offer(
               id: Guid.Parse("D5EDC0D1-407A-440D-A94D-0EF9CFF07F01"),
               bidOrder: new Order(
                   id: Guid.Parse("A80EB868-0AA4-41CC-A8BA-0857497B0C42"),
                   side: OrderSide.Bid,
                   assetId: assetId,
                   price: 10_000m,
                   status: OrderStatus.Open));

            OfferCollection offerCollection = new(offers: new Offer[] {
                firstOffer, secondOffer
            });

            offerCollection.CloseAll();

            Assert.Equal(OrderStatus.Closed.Name, firstOffer.BidOrder.Status.Name);
            Assert.Equal(OrderStatus.Closed.Name, secondOffer.BidOrder.Status.Name);
        }
    }
}
