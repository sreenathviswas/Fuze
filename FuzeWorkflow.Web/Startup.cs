using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FuzeWorkflow.Web.Startup))]
namespace FuzeWorkflow.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
