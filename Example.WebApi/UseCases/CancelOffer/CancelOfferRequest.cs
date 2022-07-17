using System.ComponentModel.DataAnnotations;

namespace Example.WebApi.UseCases.CancelOffer
{
    public class CancelOfferRequest
    {
        [Required]
        public Guid OfferId { get; set; }
    }
}
