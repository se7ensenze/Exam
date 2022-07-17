using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Sellers
{
    public class Seller
        : IHasDomainEvent
    {
        public Guid Id { get; private set; }
        public Listing? Listing { get; private set; }

        private readonly List<DomainEvent> _events;

        public ReadOnlyCollection<DomainEvent> DomainEvents => _events.AsReadOnly();
        public Trade? Trade { get; private set; }

        public Seller(Guid userId)
        {
            Id = userId;
            Listing = null;
            _events = new List<DomainEvent>();            
        }

        public Seller(Guid userId, Listing listing)
        {
            Id = userId;
            Listing = listing;
            _events = new List<DomainEvent>();
        }    

        public void CreateListing(Guid assetId, decimal price)
        {
            Listing = new Listing(assetId, price);

            _events.Add(new ListingCreatedEventArgs(this));
        }

        public void CancelListing()
        {
            Listing!.Cancel();

            _events.Add(new ListingCancelledEventArgs(this));

        }

        public void ApproveOffer(Guid offerId)
        {
            Trade = Listing!.ApproveOffer(offerId);

            _events.Add(new OfferApprovedEventArgs(this));
        }
    }
}
