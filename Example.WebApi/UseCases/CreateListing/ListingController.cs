using Example.Application.UseCases.Commands.CreateListing;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.UseCases.CreateListing
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase, IOutputPort
    {
        private readonly ICreateListingUseCase _useCase;
        private IActionResult _result;
        public ListingController(ICreateListingUseCase useCase)
        {
            _useCase = useCase;
            _useCase.SetOutputPort(this);
            _result = StatusCode(501);
        }

        [HttpPost]
        public async Task<IActionResult> CreateListing(
            [FromBody]CreateListingRequest request)
        {
            await _useCase.Execute(assetId: request.AssetId, price: request.Price);

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
