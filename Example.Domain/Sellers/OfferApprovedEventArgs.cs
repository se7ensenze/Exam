using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Sellers
{
    public class OfferApprovedEventArgs
        : DomainEvent
    {
        public Guid TradeId { get; private set; }
        public Guid AssetId { get; private set; }
        public Guid BidOrderId { get; private set; }
        public string BidOrderStatus { get; private set; }
        public Guid AskOrderId { get; private set; }
        public string AskOrderStatus { get; private set; }
        public decimal Price { get; private set; }
        public ClosedOrder[] ClosedOrders { get; private set; }


        public class ClosedOrder
        {
            public Guid OrderId { get; private set; }
            public string OrderStatus { get; private set; }

            public ClosedOrder(Order order)
            {
                OrderId = order.Id;
                OrderStatus = order.Status.Name;
            }
        }

        public OfferApprovedEventArgs(Seller seller)
        {
            var trade = seller.Trade!;
            var listing = seller.Listing!;

            TradeId = trade.Id;
            AssetId = trade.Listing.AskOrder.AssetId;
            AskOrderId = trade.Listing.AskOrder.Id;
            AskOrderStatus = trade.Listing.AskOrder.Status.Name;
            BidOrderId = trade.Offer.BidOrder.Id;
            BidOrderStatus = trade.Offer.BidOrder.Status.Name;
            Price = trade.Offer.BidOrder.Price;

            var closedOrderIdList = new List<ClosedOrder>
            {
                new ClosedOrder(listing.AskOrder)
            };

            listing.Offers
                .AsReadOnly
                .ToList()
                .ForEach(offer =>
                {
                    closedOrderIdList.Add(new ClosedOrder(offer.BidOrder));
                });

            ClosedOrders = closedOrderIdList.ToArray();
        }
    }
}
