using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain
{
    public interface IHasDomainEvent
    {
        ReadOnlyCollection<DomainEvent> DomainEvents { get; }
    }
}
