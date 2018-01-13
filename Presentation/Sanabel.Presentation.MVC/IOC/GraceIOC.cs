using Grace.DependencyInjection;

namespace Sanabel.Presentation.MVC.IOC
{
    public class GraceIOC
    {
        public static DependencyInjectionContainer Initialize()
        {
            DependencyInjectionContainer container = new DependencyInjectionContainer { new CompositionRoot() };
            return container;            
        }
    }
}