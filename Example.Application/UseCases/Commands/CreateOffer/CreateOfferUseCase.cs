using Example.Application.Services;
using Example.Domain.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Application.UseCases.Commands.CreateOffer
{
    public class CreateOfferUseCase : ICreateOfferUseCase
    {
        private IOutputPort? _outputPort;
        private readonly IUserService _userService;
        private readonly IBuyerRepository _buyerRepository;

        public CreateOfferUseCase(
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

        public async Task Execute(Guid listingId, decimal price)
        {
            var userId = _userService.GetCurrentUserId();

            try
            {
                var buyer = await _buyerRepository.Find(userId, listingId);

                buyer.CreatOffer(price);

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
