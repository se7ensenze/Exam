namespace Example.Application.UseCases.Queries.GetListings
{
    public class Result
    {
        public string ListingId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string AssetId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatedTimestamp { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string SellerId { get; set; } = string.Empty;
        public string OrderStatus { get; set; }
    }
}
