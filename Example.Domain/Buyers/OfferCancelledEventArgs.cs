namespace Example.Domain.Buyers
{
    public class OfferCancelledEventArgs
        : DomainEvent
    {
        public Guid OfferId { get; private set; }
        public Guid BidOrderId { get; private set; }
        public string BidOrderStatus { get; private set; }
        public OfferCancelledEventArgs(Buyer buyer)
        {
            OfferId = buyer.Offer!.Id;
            BidOrderId = buyer.Offer!.BidOrder.Id;
            BidOrderStatus = buyer.Offer.BidOrder.Status.Name;
        }
    }
}
