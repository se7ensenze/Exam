using Example.Application.Services;
using Example.Domain;
using Example.Domain.Sellers;

namespace Example.Application.UseCases.Commands.CancelListing
{
    public class CancelListingUseCase : ICancelListingUseCase
    {
        private readonly IUserService _userService;
        private readonly ISellerRepository _sellerRepository;
        private IOutputPort? _outputPort;

        public CancelListingUseCase(
            IUserService userService,
            ISellerRepository sellerRepository)
        {
            _userService = userService;
            _sellerRepository = sellerRepository;
        }

        public async Task Execute(Guid listingId)
        {
            var userId = _userService.GetCurrentUserId();

            var seller = await _sellerRepository.Find(userId, listingId);

            try
            {
                seller.CancelListing();

                await _sellerRepository.Save(seller);

                _outputPort?.SetOk();
            }
            catch (DomainException)
            {
                _outputPort?.SetError();
            }

        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
