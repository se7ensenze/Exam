using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Sellers
{
    public class Trade
    {
        public Guid Id { get; private set; }
        public Listing Listing { get; private set; }
        public Offer Offer { get; private set; }
        public DateTime CreatedTimestamp { get; private set; }

        public Trade(Listing listing, Offer offer)
        {
            Id = Guid.NewGuid();
            Listing = listing;
            Offer = offer;
            CreatedTimestamp = DateTime.Now;
        }

        public static Trade Create(Listing listing, Offer offer)
        {
            return new Trade(listing, offer);
        }
    }
}
