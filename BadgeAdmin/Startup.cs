using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BadgeAdmin.Startup))]
namespace BadgeAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
