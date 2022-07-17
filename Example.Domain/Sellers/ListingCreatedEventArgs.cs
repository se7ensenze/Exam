namespace Example.Domain.Sellers
{
    public class ListingCreatedEventArgs : DomainEvent
    {
        public Guid ListingId { get; private set; }
        public Guid AssetId { get; private set; }
        public decimal Price { get; private set; }
        public Guid UserId { get; private set; }
        public Guid AskOrderId { get; private set; }
        public string OrderSide { get; private set; }
        public string OrderStatus { get; private set; }
        public ListingCreatedEventArgs(Seller seller)
        {
            var listing = seller.Listing!;

            ListingId = listing.Id;
            AssetId = listing.AskOrder.AssetId;
            Price = listing.AskOrder.Price;  
            UserId = seller.Id;
            AskOrderId = listing.AskOrder.Id;
            OrderSide = listing.AskOrder.Side.Name;
            OrderStatus = listing.AskOrder.Status.Name;
        }
    }
}
