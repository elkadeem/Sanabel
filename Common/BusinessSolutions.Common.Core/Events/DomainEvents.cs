using BusinessSolutions.Common.Core.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.Events
{
    public class DomainEvents
    {
        [ThreadStatic]
        public static List<Delegate> _actions;

        private static IDependancyResolver _dependancyResolver;

        public static void Initiate(IDependancyResolver dependancyResolver)
        {
            if (dependancyResolver == null)
                throw new ArgumentNullException(nameof(dependancyResolver));

            _dependancyResolver = dependancyResolver;
        }

        public static void Register<T>(Action<T> action) where T : IDomainEvent
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (_actions == null)
                _actions = new List<Delegate>();

            _actions.Add(action);
        }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            if (_dependancyResolver != null)
            {
                var handlers = _dependancyResolver.GetAll<IHandles<T>>();
                foreach (var handler in handlers)
                    handler.Handle(args);
            }

            if (_actions != null)
            {
                foreach (var action in _actions.Where(c => c is Action<T>))
                    ((Action<T>)action)(args);
            }
        }
    }
}
