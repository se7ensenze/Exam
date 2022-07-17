using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Sellers
{
    public interface ISellerRepository
    {
        Task<Seller> Find(Guid userId);
        Task<Seller> Find(Guid userId, Guid listingId);
        Task Save(Seller seller);
    }
}
