using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Safety.Startup))]
namespace Safety
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
