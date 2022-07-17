using System.Collections;
using System.Collections.ObjectModel;

namespace Example.Domain.Sellers
{
    public class OfferCollection
    {

        private readonly List<Offer> _offerList;

        public OfferCollection()
        {
            _offerList = new List<Offer>();
        }

        public OfferCollection(IEnumerable<Offer> offers)
        {
            _offerList = new List<Offer>();

            if (offers != null && offers.Any())
            {
                _offerList.AddRange(offers);
            }
        }

        public ReadOnlyCollection<Offer> AsReadOnly => _offerList.AsReadOnly();

        public void Cancel()
        {
            foreach(var offer in _offerList)
            {
                offer.Cancel();
            }
        }

        public Offer Get(Guid offerId)
        {
            var offer = _offerList.FirstOrDefault(e => e.Id == offerId);

            if (offer == null)
            {
                throw new OfferNotFoundException();
            }


            return offer;
        }

        public void CloseAll()
        {
            foreach (var offer in _offerList)
            {
                offer.CloseOrder();
            }
        }
    }
}
