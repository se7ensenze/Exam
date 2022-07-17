using System.ComponentModel.DataAnnotations;

namespace Example.WebApi.UseCases.CancelListing
{
    public class CancelListingRequest
    {
        [Required]
        public Guid ListingId { get; set; }
    }
}