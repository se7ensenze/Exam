using System.ComponentModel.DataAnnotations;

namespace Example.Infrastructure.Entities
{
    public class Trade
    {
        [Key]
        public Guid TradeId { get; set; }
        public Guid AssetId { get; set; }
        public Guid BidOrderId { get; set; }
        public Guid AskOrderId { get; set; }
        public decimal Price { get; set; }
    }
}
