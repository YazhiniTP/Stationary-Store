using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SA47_Team12_StationeryStore.Startup))]
namespace SA47_Team12_StationeryStore
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
