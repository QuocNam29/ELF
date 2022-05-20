using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ELF.Startup))]
namespace ELF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
