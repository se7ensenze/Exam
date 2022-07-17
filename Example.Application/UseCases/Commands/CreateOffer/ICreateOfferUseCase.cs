namespace Example.Application.UseCases.Commands.CreateOffer
{
    public interface ICreateOfferUseCase
    {
        Task Execute(Guid listingId, decimal price);
        void SetOutputPort(IOutputPort outputPort);
    }
}