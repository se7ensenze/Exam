namespace Example.Domain.Buyers
{
    public class Listing
    {
        public Guid Id { get; private set; }
        public Guid AssetId { get; private set; }
        public Listing(Guid id, Guid assetId)
        {
            Id = id;
            AssetId = assetId;
        }
    }
}
