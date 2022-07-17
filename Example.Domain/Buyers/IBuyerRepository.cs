using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Buyers
{
    public interface IBuyerRepository
    {
        Task<Buyer> Find(Guid userId, Guid listingId);
        Task<Buyer> FindByOfferId(Guid userId, Guid offerId);
        Task Save(Buyer buyer);
    }
}
