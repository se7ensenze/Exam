using Example.Application.Services;
using Example.Domain.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Application.UseCases.Commands.CancelOffer
{
    public class CancelOfferUseCase : ICancelOfferUseCase
    {
        private IOutputPort? _outputPort;
        private readonly IUserService _userService;
        private readonly IBuyerRepository _buyerRepository;

        public CancelOfferUseCase(
            IUserService userService,
            IBuyerRepository buyerRepository)
        {
            _userService = userService;
            _buyerRepository = buyerRepository;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        public async Task Execute(Guid offerId)
        {
            var userId = _userService.GetCurrentUserId();

            try
            {
                var buyer = await _buyerRepository.FindByOfferId(userId, offerId);

                buyer.CancelOffer();

                await _buyerRepository.Save(buyer);

                _outputPort?.SetOk();
            }
            catch (Exception)
            {
                _outputPort?.SetError();
            }
        }
    }
}
