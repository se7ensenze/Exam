using Example.Application.UseCases.Commands.CancelListing;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.UseCases.CancelListing
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase, IOutputPort
    {
        private readonly ICancelListingUseCase _useCase;
        private IActionResult _result;
        public ListingController(ICancelListingUseCase useCase)
        {
            _useCase = useCase;
            _useCase.SetOutputPort(this);
            _result = StatusCode(501);
        }

        [HttpPatch("cancel")]
        public async Task<IActionResult> CreateListing(
            [FromBody]CancelListingRequest request)
        {
            await _useCase.Execute(request.ListingId);

            return _result;
        }

        void IOutputPort.SetError()
        {
            _result = StatusCode(500);
        }

        void IOutputPort.SetOk()
        {
            _result = Ok();
        }

    }
}
