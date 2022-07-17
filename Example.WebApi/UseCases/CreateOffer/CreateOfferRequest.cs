namespace Example.WebApi.UseCases.CreateOffer
{
    public class CreateOfferRequest
    {
        public Guid ListingId { get; set; }
        public decimal Price { get; set; }
    }
}