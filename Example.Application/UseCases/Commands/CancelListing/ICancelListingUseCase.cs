using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Application.UseCases.Commands.CancelListing
{
    public interface ICancelListingUseCase
    {
        void SetOutputPort(IOutputPort outputPort);

        Task Execute(Guid listingId);
    }
}
