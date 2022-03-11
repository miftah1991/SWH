using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SWH.Startup))]
namespace SWH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
