using Grace.DependencyInjection;

namespace Sanabel.Presentation.MVC.IOC
{
    public static class GraceIoc
    {
        public static DependencyInjectionContainer Initialize()
        {
            DependencyInjectionContainer container = new DependencyInjectionContainer { new CompositionRoot() };
            return container;            
        }
    }
}