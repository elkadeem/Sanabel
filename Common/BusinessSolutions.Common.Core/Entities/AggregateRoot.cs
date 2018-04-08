using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessSolutions.Common.Core.Events;
using BusinessSolutions.Common.Infra.Validation;

namespace BusinessSolutions.Common.Core.Entities
{
    public class AggregateRoot : Entity<Guid>
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public virtual IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        public AggregateRoot()
        {
        }

        protected virtual void AddDomainEvent(IDomainEvent newEvent)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(newEvent, nameof(newEvent));
            _domainEvents.Add(newEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
