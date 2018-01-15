using Grace.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace Sanabel.Presentation.MVC.IOC
{
    public class GraceWebApiDependencyResolver : IDependencyResolver
    {
        private IInjectionScope Container { get; set; }
        public GraceWebApiDependencyResolver(IInjectionScope container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this.Container = container;
        }

        public IDependencyScope BeginScope()
        {
            var childContainer = Container.CreateChildScope();
            return new GraceWebApiDependencyResolver(childContainer);
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return Container.Locate(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Container.LocateAll(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }
        #region IDisposable Support
        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (Container != null)
                        Container.Dispose();
                }

                disposedValue = true;
            }
        }

        ~GraceWebApiDependencyResolver()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}