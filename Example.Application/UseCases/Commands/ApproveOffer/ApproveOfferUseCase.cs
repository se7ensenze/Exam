using Example.Application.Services;
using Example.Domain.Sellers;

namespace Example.Application.UseCases.Commands.ApproveOffer
{
    public class ApproveOfferUseCase
        : IApproveOfferUseCase
    {
        private readonly IUserService _userService;
        private readonly ISellerRepository _sellerRepository;
        private IOutputPort? _outputPort;

        public ApproveOfferUseCase(
            IUserService userService,
            ISellerRepository sellerRepository)
        {
            _userService = userService;
            _sellerRepository = sellerRepository;
        }

        public async Task Execute(Guid listingId, Guid offerId)
        {
            var userId = _userService.GetCurrentUserId();

            try
            {
                var seller = await _sellerRepository.Find(userId: userId, listingId: listingId);

                seller.ApproveOffer(offerId);

                await _sellerRepository.Save(seller);

                _outputPort?.SetOk();
            }
            catch (Exception)
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
