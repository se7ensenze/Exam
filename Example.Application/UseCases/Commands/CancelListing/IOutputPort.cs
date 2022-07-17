namespace Example.Application.UseCases.Commands.CancelListing
{
    public interface IOutputPort
    {
        void SetOk();

        void SetError();
    }
}
