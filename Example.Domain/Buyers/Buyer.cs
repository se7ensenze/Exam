using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Buyers
{

    public class Buyer
        :IHasDomainEvent
    {
        public Guid Id { get; private set; }

        public Listing Listing { get; private set; }    

        public Offer? Offer { get; private set; }

        public ReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        private readonly List<DomainEvent> _domainEvents;

        public Buyer(Guid id, Listing listing)
        {
            Id = id;
            Listing = listing;
            _domainEvents = new List<DomainEvent>();
        }

        public Buyer(Guid id, Listing listing, Offer offer)
        {
            Id = id;
            Listing = listing;
            Offer = offer;
            _domainEvents = new List<DomainEvent>();

        }

        public void CreatOffer(decimal price)
        {
            Offer = new Offer(Listing.AssetId, price);

            _domainEvents.Add(new OfferCreatedEventArgs(this));
        }

        public void CancelOffer()
        {
            if (Offer == null)
            {
                throw new InvalideOperationException("No Offer were created");
            }

            Offer.BidOrder.Cancel();

            _domainEvents.Add(new OfferCancelledEventArgs(this));
        }
    }
}
