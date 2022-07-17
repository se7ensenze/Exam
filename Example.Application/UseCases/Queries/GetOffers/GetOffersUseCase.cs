using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Application.UseCases.Queries.GetOffers
{
    public class GetOffersUseCase : IGetOffersUseCase
    {
        private readonly IOfferQuery _offerQuery;
        private IOutputPort? _outputPort;

        public GetOffersUseCase(IOfferQuery offerQuery)
        {
            _offerQuery = offerQuery;
        }

        public async Task Execute(Guid listingId)
        {
            try
            {
                var result = await _offerQuery.Query(listingId);

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
