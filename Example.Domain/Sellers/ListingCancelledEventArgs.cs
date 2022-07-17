namespace Example.Domain.Sellers
{
    public class ListingCancelledEventArgs : DomainEvent
    {
        public Guid ListingId { get; private set; }
        public Guid AssetId { get; private set; }
        public decimal Price { get; private set; }
        public Guid UserId { get; private set; }
        public CancalledOrder[] CancelledOrders { get; private set; }

        public class CancalledOrder
        { 
            public Guid OrderId { get; private set; }
            public string OrderStatus { get; private set; }

            public CancalledOrder(Order order)
            {
                OrderId = order.Id;
                OrderStatus = order.Status.Name;
            }
        }

        public ListingCancelledEventArgs(Seller seller)
        {
            var listing = seller.Listing!;

            ListingId = listing.Id;
            AssetId = listing.AskOrder.AssetId;
            Price = listing.AskOrder.Price;
            UserId = seller.Id;

            var cancelledOrderIdList = new List<CancalledOrder>
            {
                new CancalledOrder(listing.AskOrder)
            };

            listing.Offers
                .AsReadOnly
                .ToList()
                .ForEach(offer =>
                {
                    cancelledOrderIdList.Add(new CancalledOrder(offer.BidOrder));
                });

            CancelledOrders = cancelledOrderIdList.ToArray();
        }
    }
}
