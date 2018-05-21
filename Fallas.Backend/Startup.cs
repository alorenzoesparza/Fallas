using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fallas.Backend.Startup))]
namespace Fallas.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
