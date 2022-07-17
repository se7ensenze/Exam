using System.ComponentModel.DataAnnotations;

namespace Example.Infrastructure.Entities
{
    public class Offer
    {
        [Key]
        public Guid OfferId { get; set; }
        public Guid ListingId { get; set; }
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTimestamp { get; set; }
    }
}
