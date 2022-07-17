using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Application.Services
{
    public interface IUserService
    {
        Guid GetCurrentUserId();
    }
}
