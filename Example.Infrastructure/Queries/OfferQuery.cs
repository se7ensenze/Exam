using Example.Application.UseCases.Queries.GetOffers;
using Example.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Infrastructure.Queries
{
    public class OfferQuery
        : IOfferQuery
    {
        private readonly ApplicationDbContext _dbContext;

        public OfferQuery(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Result[]> Query(Guid listingId)
        {
            return _dbContext.Offers
              .Join(_dbContext.Orders, offer => offer.OrderId, order => order.OrderId,
                  (offer, order) => new Result
                  {
                      CreatedTimestamp = offer.CreatedTimestamp.ToString(),
                      Description = "Offer",
                      ListingId = offer.ListingId.ToString(),
                      OfferId = offer.OfferId.ToString(),
                      OrderId = order.OrderId.ToString(),
                      Price = order.Price,
                      UserId = offer.UserId.ToString(),
                      OrderStatus = order.Status
                  })
              .ToArrayAsync();
        }
    }
}
