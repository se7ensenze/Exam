using Example.Domain.Buyers;
using Example.Domain.ValueObjects;
using Example.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.Infrastructure.Repositories
{
    public class BuyerRepository
        : IBuyerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BuyerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Buyer> Find(Guid userId, Guid listingId)
        {
            var listing = await _dbContext
                .Listings
                .FirstAsync(l => l.ListingId == listingId);

            var buyer = new Buyer(id: userId,
                listing: new Domain.Buyers.Listing(
                    id: listing.ListingId, 
                    assetId: listing.AssetId));

            return buyer;
        }

        public async Task<Buyer> FindByOfferId(Guid userId, Guid offerId)
        {

            var queryResult = await _dbContext.Offers
                .Join(inner: _dbContext.Orders,
                    outerKeySelector: offer => offer.OrderId,
                    innerKeySelector: order => order.OrderId,
                    (offer, order) => new {
                        offer.ListingId,
                        offer.OfferId,
                        order.OrderId,
                        order.Price,
                        order.AssetId,
                        order.Side,
                        order.Status
                    })
                .Where(o => o.OfferId == offerId)
                .FirstAsync();

            var buyer = new Buyer(id: userId,
                listing: new Domain.Buyers.Listing(
                    id: queryResult.ListingId,
                    assetId: queryResult.AssetId),
                offer: new Domain.Buyers.Offer(
                    id: queryResult.OfferId,
                    orderId: queryResult.OrderId,
                    assetId: queryResult.AssetId,
                    price: queryResult.Price,
                    status: OrderStatus.FromName(queryResult.Status)));

            return buyer;
        }

        public async Task Save(Buyer buyer)
        {

            foreach (var domainEvent in buyer.DomainEvents)
            {
                switch (domainEvent)
                {
                    case OfferCreatedEventArgs offerCreatedEvent:
                        SaveCreatedOffer(offerCreatedEvent);
                        break;
                    case OfferCancelledEventArgs offerCancelledEvent:
                        SaveCanceledOffer(offerCancelledEvent);
                        break;
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        #region Helpers

        private void SaveCanceledOffer(OfferCancelledEventArgs offerCancelledEvent)
        {
            var order = new Entities.Order()
            { 
                OrderId = offerCancelledEvent.BidOrderId,
                Status = offerCancelledEvent.BidOrderStatus
            };

            _dbContext.Update(order)
                .Property(o => o.Status).IsModified = true;
        }

        private void SaveCreatedOffer(OfferCreatedEventArgs offerCreatedEvent)
        {
            var newOrder = new Entities.Order()
            {
                AssetId = offerCreatedEvent.AssetId,
                OrderId = offerCreatedEvent.AskOrderId,
                Price = offerCreatedEvent.Price,
                Side = offerCreatedEvent.OrderSide,
                Status = offerCreatedEvent.OrderStatus
            };

            _dbContext.Orders.Add(newOrder);

            var newListing = new Entities.Offer()
            {
                OfferId = offerCreatedEvent.OfferId,
                ListingId = offerCreatedEvent.ListingId,
                OrderId = offerCreatedEvent.AskOrderId,
                Description = string.Empty,
                UserId = offerCreatedEvent.UserId,
                CreatedTimestamp = offerCreatedEvent.CreatedTimestamp
            };

            _dbContext.Offers.Add(newListing);
        }

        #endregion
    }
}
