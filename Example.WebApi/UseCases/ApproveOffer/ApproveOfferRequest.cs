using System.ComponentModel.DataAnnotations;

namespace Example.WebApi.UseCases.ApproveOffer
{
    public class ApproveOfferRequest
    {
        [Required]
        public Guid ListingId { get; set; }

        [Required]
        public Guid OfferId { get; set; }
    }
}