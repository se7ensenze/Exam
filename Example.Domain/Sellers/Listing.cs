using Example.Domain.ValueObjects;

namespace Example.Domain.Sellers
{
    public class Listing
    { 
        public Guid Id { get; private set; }
        public Order AskOrder { get; private set; }
        public OfferCollection Offers { get; private set; }

        public Listing(Guid assetId, decimal price)
        {
            Id = Guid.NewGuid();

            AskOrder = new Order(
                side:OrderSide.Ask, 
                assetId: assetId,
                price: price);

            Offers = new OfferCollection();
        }

        public Listing(Guid id, Order askOrder, OfferCollection offerCollection)
        {
            Id = id;
            AskOrder = askOrder;
            Offers = offerCollection;
        }

        public void Cancel()
        {
            AskOrder.Cancel();
            Offers.Cancel();
        }

        public Trade ApproveOffer(Guid offerId)
        {
            var selectedOffer = Offers.Get(offerId);

            //cancel all
            Offers.CloseAll();

            AskOrder.Close();

            return Trade.Create(listing: this, offer: selectedOffer);
        }
    }
}
