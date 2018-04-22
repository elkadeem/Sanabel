using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sanabel.Presentation.MVC.IOC
{
    public class GraceScopeModule : IHttpModule
    {
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) =>
            {
                GraceMvc.GraceDependencyResolver.CreateChildContainer();
            };

            context.EndRequest += (sender, e) =>
            {
                GraceMvc.GraceDependencyResolver.DisposeNestedContainer();
            };
        }
    }
}