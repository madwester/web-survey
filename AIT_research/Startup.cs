using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AIT_research.Startup))]
namespace AIT_research
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
