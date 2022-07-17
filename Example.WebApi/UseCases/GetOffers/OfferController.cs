using Example.Application.UseCases.Queries.GetOffers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.UseCases.GetOffers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase, IOutputPort
    {
        private readonly IGetOffersUseCase _useCase;
        private IActionResult _result;

        public OfferController(IGetOffersUseCase useCase)
        {
            _useCase = useCase;
            _useCase.SetOutputPort(this);
            _result = StatusCode(501);
        }

        [HttpGet("{listingId}")]
        public async Task<IActionResult> GetOffers([FromRoute]Guid listingId)
        {
            await _useCase.Execute(listingId);

            return _result;
        }

        void IOutputPort.SetResult(Result[] result)
        {
            _result = Ok(result);
        }
    }
}
