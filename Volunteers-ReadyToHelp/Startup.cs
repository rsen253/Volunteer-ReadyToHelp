using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Volunteers_ReadyToHelp.Startup))]
namespace Volunteers_ReadyToHelp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
