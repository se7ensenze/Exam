namespace Example.Domain.Buyers
{
    public class OfferCreatedEventArgs
        : DomainEvent
    {
        public Guid ListingId { get; private set; }
        public Guid AssetId { get; private set; }
        public decimal Price { get; private set; }
        public Guid UserId { get; private set; }
        public Guid AskOrderId { get; private set; }
        public string OrderSide { get; private set; }
        public string OrderStatus { get; private set; }
        public Guid OfferId { get; private set; }
        public OfferCreatedEventArgs(Buyer buyer)
        {
            var listing = buyer.Listing!;
            var offer = buyer.Offer!;
            OfferId = offer.Id;
            ListingId = listing.Id;
            AssetId = listing.AssetId;
            Price = offer.BidOrder.Price;
            UserId = buyer.Id;
            AskOrderId = offer.BidOrder.Id;
            OrderSide = offer.BidOrder.Side.Name;
            OrderStatus = offer.BidOrder.Status.Name;
        }
    }
}
