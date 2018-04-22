using BusinessSolutions.Common.Core.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.Events
{
    public static class DomainEvents
    {
        private static IDependancyResolver _dependancyResolver;
        public static void Initiate(IDependancyResolver dependancyResolver)
        {
            if (dependancyResolver == null)
                throw new ArgumentNullException(nameof(dependancyResolver));

            _dependancyResolver = dependancyResolver;
        }

        public static void Dispatch(IDomainEvent domainEvent)
        {
            if (domainEvent == null)
                throw new ArgumentNullException(nameof(domainEvent));

            if (_dependancyResolver != null)
            {
                var type = typeof(IHandles<>).MakeGenericType(domainEvent.GetType());
                var handlers = _dependancyResolver.GetAll(type);
                foreach (dynamic handler in handlers)
                    handler.Handle((dynamic)domainEvent);
            }

        }
    }
}
