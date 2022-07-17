namespace Example.Application.UseCases.Commands.CreateListing
{
    public interface IOutputPort
    {
        void SetOk();

        void SetError();
    }
}
