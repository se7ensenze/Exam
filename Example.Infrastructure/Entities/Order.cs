using System.ComponentModel.DataAnnotations;

namespace Example.Infrastructure.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid AssetId { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string Side { get; set; }
    }
}
