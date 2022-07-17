namespace Example.Application.UseCases.Queries.GetOffers
{
    public interface IOfferQuery
    { 
        Task<Result[]> Query(Guid listingId);
    }
}
