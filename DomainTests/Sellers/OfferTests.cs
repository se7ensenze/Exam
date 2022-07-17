using Example.Domain.Sellers;
using Example.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTests.Sellers
{
    public class OfferTests
    {
        [Fact]
        public void When_Call_CloseOrder_OrderStatus_Should_Be_Closed()
        {
            var bidOrder = new Order(
                    id: Guid.Parse("6C78BFC0-0DF6-43E6-8A6E-1AA44773B5EF"),
                    side: OrderSide.Bid,
                    assetId: Guid.Parse("BE7071C6-108E-417D-9366-4D831590E7F7"),
                    price: 99_999m,
                    status: OrderStatus.Open);

            Offer offer = new(id: Guid.Parse("F318CAD1-923E-412A-836B-21CF87CC4705"),
                bidOrder: bidOrder);

            offer.CloseOrder();

            Assert.Equal(OrderStatus.Closed.Name, bidOrder.Status.Name);
        }

        [Fact]
        public void When_OrderStatus_Is_Cancelled_Open_Call_CloseOrder_OrderStatus_Should_Not_Changed()
        {
            var bidOrder = new Order(
                    id: Guid.Parse("5B5CAF58-7177-4B93-A61E-956233E8ABDD"),
                    side: OrderSide.Bid,
                    assetId: Guid.Parse("12F01E82-ADCA-4247-9A47-71FFBBBE7FED"),
                    price: 99_999m,
                    status: OrderStatus.Cancelled);

            Offer offer = new(id: Guid.Parse("39D3C245-767C-474A-A9C2-5808A042C260"),
                bidOrder: bidOrder);

            offer.CloseOrder();

            Assert.Equal(OrderStatus.Cancelled.Name, bidOrder.Status.Name);
        }

        [Fact]
        public void When_Call_Cancel_OrderStatus_Should_Be_Cancelled()
        {
            var bidOrder = new Order(
                    id: Guid.Parse("6C78BFC0-0DF6-43E6-8A6E-1AA44773B5EF"),
                    side: OrderSide.Bid,
                    assetId: Guid.Parse("BE7071C6-108E-417D-9366-4D831590E7F7"),
                    price: 99_999m,
                    status: OrderStatus.Open);

            Offer offer = new(id: Guid.Parse("F318CAD1-923E-412A-836B-21CF87CC4705"),
                bidOrder: bidOrder);

            offer.Cancel();

            Assert.Equal(OrderStatus.Cancelled.Name, bidOrder.Status.Name);
        }

        [Fact]
        public void When_OrderStatus_Is_Closed_Open_Call_Cancel_OrderStatus_Should_Not_Changed()
        {
            var bidOrder = new Order(
                    id: Guid.Parse("5B5CAF58-7177-4B93-A61E-956233E8ABDD"),
                    side: OrderSide.Bid,
                    assetId: Guid.Parse("12F01E82-ADCA-4247-9A47-71FFBBBE7FED"),
                    price: 99_999m,
                    status: OrderStatus.Closed);

            Offer offer = new(id: Guid.Parse("39D3C245-767C-474A-A9C2-5808A042C260"),
                bidOrder: bidOrder);

            offer.Cancel();

            Assert.Equal(OrderStatus.Closed.Name, bidOrder.Status.Name);
        }
    }
}
