using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Buyers
{
    public class InvalideOperationException
        :Exception
    {
        public InvalideOperationException(string message) : base(message)
        { 
        }
    }
}
