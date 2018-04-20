using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EFSecurityShell.Startup))]
namespace EFSecurityShell
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
