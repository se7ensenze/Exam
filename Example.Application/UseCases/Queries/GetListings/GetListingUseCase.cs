using Example.Application.Services;

namespace Example.Application.UseCases.Queries.GetListings
{
    public class GetListingUseCase : IGetListingUseCase
    {
        private readonly IUserService _userService;
        private readonly IListingQuery _listingQuery;
        private IOutputPort? _outputPort;

        public GetListingUseCase(
            IUserService userService,
            IListingQuery listingQuery)
        {
            _userService = userService;
            _listingQuery = listingQuery;
        }

        public async Task Execute()
        {

            try
            {
                var result = await _listingQuery.Query();

                _outputPort?.SetResult(result);
            }
            catch (Exception)
            {
                _outputPort?.SetResult(Array.Empty<Result>());
            }
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
