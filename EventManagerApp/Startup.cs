using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventManagerApp.Startup))]
namespace EventManagerApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
