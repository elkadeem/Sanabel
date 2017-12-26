using Microsoft.Owin;
using Owin;
using Sanabel.Presentation.MVC.IOC;

[assembly: OwinStartupAttribute(typeof(Sanabel.Presentation.MVC.Startup))]
namespace Sanabel.Presentation.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GraceIOC.Config();
        }
    }
}
