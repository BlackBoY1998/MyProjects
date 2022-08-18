using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DempProject.Startup))]
namespace DempProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
