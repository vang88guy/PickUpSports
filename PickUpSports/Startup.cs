using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PickUpSports.Startup))]
namespace PickUpSports
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
