using Example.Application.UseCases.Queries.GetListings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.UseCases.GetListings
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase, IOutputPort
    {
        private readonly IGetListingUseCase _useCase;
        private IActionResult _result;
        public ListingController(IGetListingUseCase useCase)
        {
            _useCase = useCase;
            _useCase.SetOutputPort(this);
            _result = StatusCode(501);
        }

        [HttpGet]
        public async Task<IActionResult> GetListings()
        {
            await _useCase.Execute();

            return _result;
        }

        void IOutputPort.SetResult(Result[] result)
        {
            _result = Ok(result);
        }
    }
}
