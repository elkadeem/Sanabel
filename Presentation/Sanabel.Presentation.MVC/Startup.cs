using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sanabel.Presentation.MVC.Startup))]
namespace Sanabel.Presentation.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
