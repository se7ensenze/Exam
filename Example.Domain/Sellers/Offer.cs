namespace Example.Domain.Sellers
{
    public class Offer
    {

        public Guid Id { get; private set; }
        public Order BidOrder { get; private set; }

        public Offer(Guid id, Order bidOrder)
        {
            Id = id;
            BidOrder = bidOrder;
        }

        public void Cancel()
        {
            BidOrder.Cancel();
        }

        public void CloseOrder()
        {
            BidOrder.Close();
        }
    }
}
