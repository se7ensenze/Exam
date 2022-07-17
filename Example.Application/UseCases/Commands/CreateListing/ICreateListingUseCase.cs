using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Application.UseCases.Commands.CreateListing
{
    public interface ICreateListingUseCase
    {
        void SetOutputPort(IOutputPort outputPort);

        Task Execute(Guid assetId, decimal price);
    }
}
