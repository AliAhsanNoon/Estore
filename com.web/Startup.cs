using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(com.web.Startup))]
namespace com.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
