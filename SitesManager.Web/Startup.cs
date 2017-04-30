using Microsoft.Owin;
using Owin;
using SitesManager.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace SitesManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}