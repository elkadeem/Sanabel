using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.Events
{
    public interface IHandles<T> where T : IDomainEvent
    {
        void Handle(IDomainEvent domainEvent);
    }
}
