﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using BusinessSolutions.Common.Core.Events;
using Grace.DependencyInjection;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Sanabel.Presentation.MVC;
using Sanabel.Presentation.MVC.IOC;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(GraceMvc), "Start")]
[assembly: ApplicationShutdownMethod(typeof(GraceMvc), "End")]
namespace Sanabel.Presentation.MVC
{
    public static class GraceMvc
    {
        public static GraceDependencyResolver GraceDependencyResolver { get; private set; }
        private static GraceWebApiDependencyResolver _graceWebApiDependencyResolver;
        public static void End()
        {
            if (GraceDependencyResolver != null)
                GraceDependencyResolver.Dispose();
            if (_graceWebApiDependencyResolver != null)
                _graceWebApiDependencyResolver.Dispose();
        }

        public static void Start()
        {
            DependencyInjectionContainer container = GraceIoc.Initialize();
            GraceDependencyResolver = new GraceDependencyResolver(container);
            DependencyResolver.SetResolver(GraceDependencyResolver);

            _graceWebApiDependencyResolver = new GraceWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = _graceWebApiDependencyResolver;
            DynamicModuleUtility.RegisterModule(typeof(GraceScopeModule));

            DomainEvents.Initiate(GraceDependencyResolver);
        }
    }
}