using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(prueba.Startup))]
namespace prueba
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
