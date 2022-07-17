using Example.Application.UseCases.Queries.GetListings;
using Example.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Infrastructure.Queries
{
    public class ListingQuery
        : IListingQuery
    {
        private readonly ApplicationDbContext _myContext;

        public ListingQuery(ApplicationDbContext myContext)
        {
            _myContext = myContext;
        }

        public Task<Result[]> Query()
        {
            return _myContext.Listings
                .Join(_myContext.Orders, list => list.OrderId, order => order.OrderId,
                    (list, order) => new Result { 
                        AssetId = order.AssetId.ToString(),
                        CreatedTimestamp = list.CreatedTimestamp.ToString(),
                        Description = String.Empty,
                        ListingId = list.ListingId.ToString(),
                        OrderId = list.OrderId.ToString(),
                        Price = order.Price.ToString(),
                        SellerId = list.UserId.ToString(),
                        OrderStatus = order.Status
                    }) 
                .OrderBy(order => order.Price)
                .ThenByDescending(order => order.CreatedTimestamp)
                .ToArrayAsync();
        }
    }
}
