using Example.Application.UseCases.Commands.CancelOffer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.UseCases.CancelOffer
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase, IOutputPort
    {
        private readonly ICancelOfferUseCase _useCase;
        private IActionResult _result;

        public OfferController(ICancelOfferUseCase useCase)
        {
            _useCase = useCase;
            _useCase.SetOutputPort(this);
            _result = StatusCode(501);
        }

        [HttpPatch("cancel")]
        public async Task<IActionResult> CancelOffer([FromBody]CancelOfferRequest request)
        {
            await _useCase.Execute(request.OfferId);

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
