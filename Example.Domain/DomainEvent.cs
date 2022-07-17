using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain
{
    public abstract class DomainEvent
    {
        public Guid Id { get; private set; }
        public DateTime CreatedTimestamp { get; private set; }

        public DomainEvent()
        { 
            Id = Guid.NewGuid();
            CreatedTimestamp = DateTime.Now;
        }
    }

    
}
