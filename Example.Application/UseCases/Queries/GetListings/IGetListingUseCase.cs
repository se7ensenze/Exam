using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Application.UseCases.Queries.GetListings
{
    public interface IGetListingUseCase
    {
        void SetOutputPort(IOutputPort outputPort);
        Task Execute();
    }
}
