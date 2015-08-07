using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FuzeFormEngine.Startup))]
namespace FuzeFormEngine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
