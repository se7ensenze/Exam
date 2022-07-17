using System.ComponentModel.DataAnnotations;

namespace Example.WebApi.UseCases.CreateListing
{
    public class CreateListingRequest
    {
        [Required]
        public Guid AssetId { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}