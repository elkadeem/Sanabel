using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grace.DependencyInjection;
using Grace.MVC.DependencyInjection;

namespace Sanabel.Presentation.MVC.IOC
{
    public class CompositionRoot : Grace.DependencyInjection.IConfigurationModule
    {
        public void Configure(IExportRegistrationBlock registrationBlock)
        {
            var assembly =  System.Reflection.Assembly.Load("CommonSettings.DAL");            
            registrationBlock.ExportAssembly(assembly)
                .ExportAttributedTypes();
            
            assembly = System.Reflection.Assembly.Load("CommonSettings.BLL");
            registrationBlock.ExportAssembly(assembly)
                .ExportAttributedTypes();

            var logger = NLog.LogManager.CreateNullLogger();
            registrationBlock.ExportInstance(logger).As<NLog.ILogger>()
                .Lifestyle.Singleton();
            registrationBlock.ExportController(this.GetType().Assembly.ExportedTypes);
        }
    }
}