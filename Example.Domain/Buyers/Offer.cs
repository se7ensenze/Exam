using Example.Domain.ValueObjects;

namespace Example.Domain.Buyers
{
    public class Offer
    {
        public Offer(Guid assetId, decimal price)
        {
            Id = Guid.NewGuid();
            BidOrder = new Order(
                side: OrderSide.Bid, 
                assetId: assetId,
                price: price);
        }

        public Offer(Guid id, Guid orderId, Guid assetId, decimal price, OrderStatus status)
        {
            Id = id;
            BidOrder = new Order(
                id: orderId,
                side: OrderSide.Bid,
                assetId: assetId,
                price: price,
                status: status);
        }

        public Guid Id { get; private set; }
        public Order BidOrder { get; set; }
    }
}
