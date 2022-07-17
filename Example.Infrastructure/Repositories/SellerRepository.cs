using Example.Domain.Sellers;
using Example.Domain.ValueObjects;
using Example.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.Infrastructure.Repositories
{

    public class SellerRepository
        : ISellerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SellerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Seller> Find(Guid userId)
        {
            return Task.FromResult(
                result: new Seller(userId));
        }

        public async Task<Seller> Find(Guid userId, Guid listingId)
        {

            #region Query

            var listingQueryResult = await _dbContext.Listings
                   .Join(_dbContext.Orders, l => l.OrderId, o => o.OrderId,
                       (l, o) => new {
                           l.ListingId,
                           l.UserId,
                           o.AssetId,
                           o.Price,
                           o.OrderId,
                           o.Side,
                           o.Status
                       })
                   .Where(e => e.UserId == userId && e.ListingId == listingId)
                   .FirstOrDefaultAsync();

            if (listingQueryResult == null)
            {
                throw new ListingNotFoundException();
            }

            var offerQueryResult = await _dbContext.Offers
               .Join(inner: _dbContext.Orders,
                    outerKeySelector: offer => offer.OrderId,
                    innerKeySelector: order => order.OrderId,
                    resultSelector: (offer, order) => new
                    {
                        offer.ListingId,
                        offer.OfferId,
                        order.OrderId,
                        order.AssetId,
                        order.Price,
                        order.Status,
                        order.Side,
                        offer.Description,
                        offer.UserId
                    })
               .Where(e => e.UserId == userId && e.ListingId == listingId)
               .ToListAsync();

            #endregion

            var loadedOffers = offerQueryResult
               .Select(e => new Domain.Sellers.Offer(
                   id: e.OfferId,
                   bidOrder: new Domain.Sellers.Order(
                                id: e.OrderId,
                                side: OrderSide.FromName(e.Side),
                                assetId: e.AssetId,
                                price: e.Price,
                                status: OrderStatus.FromName(e.Status)
                                ))).ToArray();

            var loadedAskOrder = new Domain.Sellers.Order(
                id: listingQueryResult.OrderId,
                side: OrderSide.FromName(listingQueryResult.Side),
                assetId: listingQueryResult.AssetId,
                price: listingQueryResult.Price,
                status: OrderStatus.FromName(listingQueryResult.Status));

            var loadedList = new Domain.Sellers.Listing(
                id: listingQueryResult.ListingId,
                askOrder: loadedAskOrder,
                offerCollection: new OfferCollection(loadedOffers));

            return new Seller(userId, listing: loadedList);

        }


        public async Task Save(Seller seller)
        {
            foreach (var domainEvent in seller.DomainEvents)
            {
                switch (domainEvent)
                {
                    case ListingCreatedEventArgs listingCreatedEvent:
                        SaveCreateListing(listingCreatedEvent);
                        break;
                    case ListingCancelledEventArgs listingCancelledEvent:
                        SaveCancelListEvent(listingCancelledEvent);
                        break;
                    case OfferApprovedEventArgs offerApprovedEvent:
                        SaveApprovedOffer(offerApprovedEvent);
                        break;
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        #region Helper functions

        private void SaveApprovedOffer(OfferApprovedEventArgs offerApprovedEvent)
        {

            offerApprovedEvent.ClosedOrders
                .ToList()
                .ForEach(e =>
                {
                    var order = new Entities.Order()
                    {
                        OrderId = e.OrderId,
                        Status = e.OrderStatus
                    };

                    _dbContext.Orders.Update(order)
                        .Property(o => o.Status).IsModified = true;
                });

            var trade = new Entities.Trade()
            {
                TradeId = offerApprovedEvent.TradeId,
                AskOrderId = offerApprovedEvent.AskOrderId,
                BidOrderId = offerApprovedEvent.BidOrderId,
                AssetId = offerApprovedEvent.AssetId,
                Price = offerApprovedEvent.Price
            };

            _dbContext.Add(trade);
        }

        private void SaveCancelListEvent(ListingCancelledEventArgs listingCancelledEvent)
        {
            listingCancelledEvent.CancelledOrders
                .ToList()
                .ForEach(e =>
                {
                    var order = new Entities.Order()
                    {
                        OrderId = e.OrderId,
                        Status = e.OrderStatus
                    };

                    _dbContext.Orders.Update(order)
                        .Property(o => o.Status).IsModified = true;
                });
        }

        private void SaveCreateListing(ListingCreatedEventArgs listingCreatedEvent)
        {
            var newOrder = new Entities.Order()
            {
                AssetId = listingCreatedEvent.AssetId,
                OrderId = listingCreatedEvent.AskOrderId,
                Price = listingCreatedEvent.Price,
                Side = listingCreatedEvent.OrderSide,
                Status = listingCreatedEvent.OrderStatus
            };

            _dbContext.Orders.Add(newOrder);

            var newListing = new Entities.Listing()
            {
                ListingId = listingCreatedEvent.ListingId,
                OrderId = listingCreatedEvent.AskOrderId,
                AssetId = listingCreatedEvent.AssetId,
                UserId = listingCreatedEvent.UserId,
                CreatedTimestamp = listingCreatedEvent.CreatedTimestamp
            };

            _dbContext.Listings.Add(newListing);
        }

        #endregion

    }
}
