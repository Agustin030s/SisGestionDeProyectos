using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SisGestionDeProyectos.Startup))]
namespace SisGestionDeProyectos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
