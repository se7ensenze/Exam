using Example.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Infrastructure.Services
{
    public class FakeUserService
        : IUserService
    {
        Guid _currentUserId = Guid.Parse("D32C4A42-7AF6-4532-8ED6-76E4845834D1");
        public Guid GetCurrentUserId()
        {
            return _currentUserId;
        }
    }
}
