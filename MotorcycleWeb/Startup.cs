using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MotorcycleWeb.Startup))]
namespace MotorcycleWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
