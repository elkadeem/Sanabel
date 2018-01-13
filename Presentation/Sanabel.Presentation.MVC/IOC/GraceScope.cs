using Grace.DependencyInjection;
using Grace.DependencyInjection.Impl;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Sanabel.Presentation.MVC.IOC
{
    public class GraceDependencyResolver : IDependencyResolver
    {
        private const string NestedScopeKey = "Nested.Grace.Container";
        public IInjectionScope Container { get; set; }

        private HttpContextBase HttpContext
        {
            get
            {
                return System.Web.HttpContext.Current == null ? null : new HttpContextWrapper(System.Web.HttpContext.Current);
            }
        }

        public IInjectionScope CurrentNestedContainer
        {
            get
            {
                return HttpContext?.Items[NestedScopeKey] as InjectionScope;
            }
            set
            {
                if (HttpContext != null)
                    HttpContext.Items[NestedScopeKey] = value;
            }
        }

        public void CreateChildContainer()
        {
            CurrentNestedContainer = Container.CreateChildScope();
        }

        public void DisposeNestedContainer()
        {
            if(CurrentNestedContainer != null)
            {
                CurrentNestedContainer.Dispose();
                CurrentNestedContainer = null;
            }
        }

        public GraceDependencyResolver(IInjectionScope container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this.Container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return (CurrentNestedContainer?? Container).Locate(serviceType);
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
                return (CurrentNestedContainer?? Container).LocateAll(serviceType);
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
                    DisposeNestedContainer();
                    if (Container != null)
                        Container.Dispose();
                }

                disposedValue = true;
            }
        }

        ~GraceDependencyResolver()
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