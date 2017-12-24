using Grace.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grace.MVC.DependencyInjection;

namespace Sanabel.Presentation.IOC
{
    public class GraceIOC
    {
        private static DependencyInjectionContainer container;
        public static void Config()
        {
            
            container = new DependencyInjectionContainer { new CompositionRoot() };
            IControllerFactory controllerFactory = new Grace.MVC.Extensions.DisposalScopeControllerActivator(container);

            container.Configure(c =>
              c.ExportInstance(controllerFactory).As<IControllerFactory>());

            //DependencyResolver.SetResolver(new Grace.MVC.Extensions.GraceDependencyResolver(container));
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }


    }
}