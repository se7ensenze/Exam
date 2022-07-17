using Example.Domain.ValueObjects;

namespace Example.Domain.Buyers
{
    public class Order
    {
        public Guid Id { get; private set; }
        public OrderSide Side { get; private set; }
        public Guid AssetId { get; private set; }
        public decimal Price { get; private set; }
        public OrderStatus Status { get; private set; }

        public void Cancel()
        {
            if (Status.CanBeCancelled)
            {
                Status = OrderStatus.Cancelled;
            }
        }

        public void Close()
        {
            if (Status.CanBeClosed)
            {
                Status = OrderStatus.Closed;
            }
        }

        public Order(OrderSide side, Guid assetId, decimal price)
        {
            Id = Guid.NewGuid();
            Side = side;
            AssetId = assetId;
            Price = price;
            Status = OrderStatus.Open;
        }

        public Order(Guid id, OrderSide side, Guid assetId, decimal price, OrderStatus status)
        {
            Id = id;
            Side = side;
            AssetId = assetId;
            Price = price;
            Status = status;
        }
    }
}
