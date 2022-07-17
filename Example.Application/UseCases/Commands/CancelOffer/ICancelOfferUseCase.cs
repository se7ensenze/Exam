namespace Example.Application.UseCases.Commands.CancelOffer
{
    public interface ICancelOfferUseCase
    {
        Task Execute(Guid offerId);
        void SetOutputPort(IOutputPort outputPort);
    }
}