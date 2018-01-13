using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Grace.DependencyInjection;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Sanabel.Presentation.MVC;
using Sanabel.Presentation.MVC.IOC;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(GraceMVC), "Start")]
[assembly: ApplicationShutdownMethod(typeof(GraceMVC), "End")]
namespace Sanabel.Presentation.MVC
{
    public static class GraceMVC
    {
        public static GraceDependencyResolver GraceDependencyResolver { get; private set; }

        public static void End()
        {
            if (GraceDependencyResolver != null)
                GraceDependencyResolver.Dispose();
        }

        public static void Start()
        {
            DependencyInjectionContainer container = GraceIOC.Initialize();
            GraceDependencyResolver = new GraceDependencyResolver(container);
            DependencyResolver.SetResolver(GraceDependencyResolver);
            DynamicModuleUtility.RegisterModule(typeof(GraceScopeModule));
        }
    }
}