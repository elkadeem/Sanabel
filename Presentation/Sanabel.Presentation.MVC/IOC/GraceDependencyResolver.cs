using Grace.DependencyInjection;
using Grace.DependencyInjection.Impl;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http.Dependencies;
using System.Web.Mvc;

namespace Sanabel.Presentation.MVC.IOC
{
    public class GraceDependencyResolver : System.Web.Mvc.IDependencyResolver
        , BusinessSolutions.Common.Core.IOC.IDependancyResolver
        , IDisposable
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

        public GraceDependencyResolver(IInjectionScope container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this.Container = container;
        }

        public void CreateChildContainer()
        {
            CurrentNestedContainer = Container.CreateChildScope();
        }

        public void DisposeNestedContainer()
        {
            if (CurrentNestedContainer != null)
            {
                CurrentNestedContainer.Dispose();
                CurrentNestedContainer = null;
            }
        }

        public object GetService(Type serviceType)
        {
            return Get(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return GetAll(serviceType);
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

        public T Get<T>()
        {
            try
            {
                return (CurrentNestedContainer ?? Container).Locate<T>();
            }
            catch
            {
                return default(T);
            }
        }

        public IEnumerable<T> GetAll<T>()
        {
            try
            {
                return (CurrentNestedContainer ?? Container).LocateAll<T>();
            }
            catch
            {
                return new List<T>();
            }
        }

        public object Get(Type type)
        {
            try
            {
                return (CurrentNestedContainer ?? Container).Locate(type);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetAll(Type type)
        {
            try
            {
                return (CurrentNestedContainer ?? Container).LocateAll(type);
            }
            catch
            {
                return new List<object>();
            }
        }
        #endregion
    }
}