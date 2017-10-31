using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ged.Startup))]
namespace Ged
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
