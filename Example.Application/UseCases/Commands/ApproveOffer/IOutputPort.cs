namespace Example.Application.UseCases.Commands.ApproveOffer
{
    public interface IOutputPort
    {
        void SetOk();
        void SetError();
    }
}
