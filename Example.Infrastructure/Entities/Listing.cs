using System.ComponentModel.DataAnnotations;

namespace Example.Infrastructure.Entities
{
    public class Listing
    {
        [Key]
        public Guid ListingId { get; set; }
        public Guid OrderId { get; set; }
        public Guid AssetId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedTimestamp { get; set; }
    }
}
