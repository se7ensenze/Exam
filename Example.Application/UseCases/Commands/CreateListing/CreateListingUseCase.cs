using Example.Application.Services;
using Example.Domain;
using Example.Domain.Sellers;

namespace Example.Application.UseCases.Commands.CreateListing
{
    public class CreateListingUseCase : ICreateListingUseCase
    {
        private readonly IUserService _userService;
        private readonly ISellerRepository _sellerRepository;
        private IOutputPort? _outputPort;

        public CreateListingUseCase(
            IUserService userService,
            ISellerRepository sellerRepository)
        {
            _userService = userService;
            _sellerRepository = sellerRepository;
        }

        public async Task Execute(Guid assetId, decimal price)
        {
            var userId = _userService.GetCurrentUserId();

            var seller = await _sellerRepository.Find(userId: userId);

            try
            {
                seller.CreateListing(assetId, price);

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
