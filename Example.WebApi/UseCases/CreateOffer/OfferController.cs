using Example.Application.UseCases.Commands.CreateOffer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.UseCases.CreateOffer
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase, IOutputPort
    {
        private readonly ICreateOfferUseCase _useCase;
        private IActionResult _result;

        public OfferController(ICreateOfferUseCase useCase)
        {
            _useCase = useCase;
            _useCase.SetOutputPort(this);
            _result = StatusCode(501);
        }

        [HttpPost()]
        public async Task<IActionResult> CancelOffer([FromBody]CreateOfferRequest request)
        {
            await _useCase.Execute(request.ListingId, request.Price);

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
