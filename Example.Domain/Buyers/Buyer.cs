using System.Collections.ObjectModel;

namespace Example.Domain.Buyers
{

    public class Buyer
        :IHasDomainEvent
    {
        public Guid Id { get; }

        public Listing Listing { get; }    

        public Offer? Offer { get; private set; }

        public ReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        private readonly List<DomainEvent> _domainEvents;

        public Buyer(Guid id, Listing listing, Offer? offer)
        {
            Id = id;
            Listing = listing;
            Offer = offer;
            _domainEvents = new List<DomainEvent>();

        }

        public void CreatOffer(decimal price)
        {
            if (Offer != null)
            {
                throw new InvalideOperationException("Offer is already created");
            }

            Offer = new Offer(Listing.AssetId, price);

            _domainEvents.Add(new OfferCreatedEventArgs(this));
        }

        public void CancelOffer()
        {
            if (Offer == null)
            {
                throw new InvalideOperationException("No Offer were created");
            }

            Offer.Cancel();

            _domainEvents.Add(new OfferCancelledEventArgs(this));
        }
    }
}
