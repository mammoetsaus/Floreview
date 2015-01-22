using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Floreview.Startup))]
namespace Floreview
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
