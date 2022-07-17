namespace Example.Application.UseCases.Queries.GetOffers
{
    public class Result
    {
        public string OfferId { get; set; }
        public string ListingId { get; set; }
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string CreatedTimestamp { get; set; }
        public decimal Price { get; set; }
        public string OrderStatus { get; set; }
    }
    
}
