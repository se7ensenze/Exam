using Example.Application.UseCases.Commands.ApproveOffer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.UseCases.ApproveOffer
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase, IOutputPort
    {
        private readonly IApproveOfferUseCase _useCase;
        private IActionResult _result;

        public OfferController(IApproveOfferUseCase useCase)
        {
            _useCase = useCase;
            _useCase.SetOutputPort(this);
            _result = StatusCode(501);
        }

        [HttpPatch("approve")]
        public async Task<IActionResult> CancelOffer([FromBody]ApproveOfferRequest request)
        {
            await _useCase.Execute(request.ListingId, request.OfferId);

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
