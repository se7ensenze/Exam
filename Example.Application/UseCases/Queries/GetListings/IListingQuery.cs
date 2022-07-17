namespace Example.Application.UseCases.Queries.GetListings
{
    public interface IListingQuery
    {
        Task<Result[]> Query();
    }
}
