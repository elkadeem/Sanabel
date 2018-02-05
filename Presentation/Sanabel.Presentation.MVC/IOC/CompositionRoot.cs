using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grace.DependencyInjection;
using Microsoft.AspNet.Identity;
using Security.AspIdentity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Http;
using Sanabel.Presentation.MVC.Logs;
using BusinessSolutions.Common.Infra.Log;

namespace Sanabel.Presentation.MVC.IOC
{
    public class CompositionRoot : Grace.DependencyInjection.IConfigurationModule
    {
        public void Configure(IExportRegistrationBlock registrationBlock)
        {
            var assembly = System.Reflection.Assembly.Load("BusinessSolutions.Common.Infra");
            registrationBlock.ExportAssembly(assembly)
                .ByInterfaces();

            assembly = System.Reflection.Assembly.Load("CommonSettings.DAL");
            registrationBlock.ExportAssembly(assembly)
                .ExportAttributedTypes();

            assembly = System.Reflection.Assembly.Load("CommonSettings.BLL");
            registrationBlock.ExportAssembly(assembly)
                .ExportAttributedTypes();

            assembly = System.Reflection.Assembly.Load("Sanabel.Security.Infra");
            registrationBlock.ExportAssembly(assembly).ByInterfaces(c => c.Name.EndsWith("Repository")
                || c.Name.EndsWith("UnitOfWork"));

            assembly = System.Reflection.Assembly.Load("Security.AspIdentity");
            registrationBlock.ExportAssembly(assembly)
                .ByInterfaces();
            
            assembly = System.Reflection.Assembly.Load("Sanabel.Security.Application");
            registrationBlock.ExportAssembly(assembly)
                .ByInterfaces();

            assembly = System.Reflection.Assembly.Load("Sanable.Cases.Infra");
            registrationBlock.ExportAssembly(assembly)
                .ByInterfaces();

            assembly = System.Reflection.Assembly.Load("Sanabel.Cases.App");
            registrationBlock.ExportAssembly(assembly)
                .ByInterfaces();

            assembly = System.Reflection.Assembly.Load("Sanabel.Volunteers.Infra");
            registrationBlock.ExportAssembly(assembly)
                .ByInterfaces();

            assembly = System.Reflection.Assembly.Load("Sanabel.Volunteers.Application");
            registrationBlock.ExportAssembly(assembly)
                .ByInterfaces();


            registrationBlock.ExportFactory<IAuthenticationManager>(() => HttpContext.Current.GetOwinContext().Authentication);

            var logger = NLog.LogManager.CreateNullLogger();
            registrationBlock.ExportInstance(logger).As<NLog.ILogger>()
                .Lifestyle.Singleton();

            registrationBlock.ExportAs<AppLogger, ILogger>();

            registrationBlock.ExportAssemblyContaining<CompositionRoot>()
                .BasedOn<Controller>();

            registrationBlock.ExportAssemblyContaining<CompositionRoot>()
                .BasedOn<ApiController>();

        }
    }
}