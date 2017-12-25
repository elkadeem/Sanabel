using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sanabel.Presentation.Startup))]
namespace Sanabel.Presentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            ConfigureAuth(app);
        }
    }
}
