using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grace.DependencyInjection;
using Grace.MVC.DependencyInjection;
using Microsoft.AspNet.Identity;
using Security.AspIdentity;
using Microsoft.Owin.Security;

namespace Sanabel.Presentation.MVC.IOC
{
    public class CompositionRoot : Grace.DependencyInjection.IConfigurationModule
    {
        public void Configure(IExportRegistrationBlock registrationBlock)
        {
            var assembly = System.Reflection.Assembly.Load("CommonSettings.DAL");
            registrationBlock.ExportAssembly(assembly)
                .ExportAttributedTypes();

            assembly = System.Reflection.Assembly.Load("CommonSettings.BLL");
            registrationBlock.ExportAssembly(assembly)
                .ExportAttributedTypes();

            assembly = System.Reflection.Assembly.Load("Security.DataAccessLayer");
            registrationBlock.ExportAssembly(assembly).ByInterfaces(c => c.Name.EndsWith("Repository")
                || c.Name.EndsWith("UnitOfWork"));

            assembly = System.Reflection.Assembly.Load("Security.AspIdentity");
            registrationBlock.ExportAssembly(assembly)
                .ByInterfaces();
            //registrationBlock.ExportAssembly(assembly).BasedOn<UserManager<ApplicationUser, Guid>>();
            //registrationBlock.ExportAssembly(assembly).BasedOn<RoleManager<ApplicationRole, Guid>>();

            registrationBlock.ExportFactory<IAuthenticationManager>(() => HttpContext.Current.GetOwinContext().Authentication);

            var logger = NLog.LogManager.CreateNullLogger();
            registrationBlock.ExportInstance(logger).As<NLog.ILogger>()
                .Lifestyle.Singleton();
            registrationBlock.ExportController(this.GetType().Assembly.ExportedTypes);
        }
    }
}