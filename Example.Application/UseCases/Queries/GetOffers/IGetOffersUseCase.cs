namespace Example.Application.UseCases.Queries.GetOffers
{
    public interface IGetOffersUseCase
    {
        Task Execute(Guid listingId);
        void SetOutputPort(IOutputPort outputPort);
    }
}