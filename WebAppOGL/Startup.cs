using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppOGL.Startup))]
namespace WebAppOGL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
