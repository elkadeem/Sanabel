using BusinessSolutions.Common.Core.Events;
using BusinessSolutions.Common.Core.IOC;
using Sanabel.Security.Infra;
using Security.AspIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.IntegrationTest.Common
{
    public class DependancyResolver : IDependancyResolver
    {
        private ApplicationUserManager _userManager;


        private static List<Type> _handlers;
        public DependancyResolver()
        {
            InitiateUserManagement();
            _handlers = Assembly.Load("Sanabel.Volunteers.Infra")
               .GetTypes()
               .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandles<>)))
               .ToList();
        }

        public T Get<T>()
        {
            throw new NotImplementedException();
        }

        public object Get(Type type)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetAll(Type type)
        {
            List<object> result = new List<object>();
            foreach(var handlerType in _handlers)
            {
                bool canHandleEvent = handlerType.GetInterfaces()
                   .Any(x => x.IsGenericType
                       && x.GetGenericTypeDefinition() == typeof(IHandles<>)
                       && x.GenericTypeArguments[0] == type.GenericTypeArguments[0]);

                if (canHandleEvent)
                {
                    var newHandler =  Activator.CreateInstance(handlerType, _userManager);
                    result.Add(newHandler);
                }
            }

            return result;
        }

        private void InitiateUserManagement()
        {
            var dataContext = new SecurityContext();
            var securityUnitOfWork = new SecurityUnitOfWork(dataContext);
            var userStore = new UserStore(securityUnitOfWork);
            _userManager = new ApplicationUserManager(userStore, null)
            {
                UserLockoutEnabledByDefault = false,
                MaxFailedAccessAttemptsBeforeLockout = 3,
                DefaultAccountLockoutTimeSpan = new TimeSpan(0, 10, 0),

            };
        }
    }
}
