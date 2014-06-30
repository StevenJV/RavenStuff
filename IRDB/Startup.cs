using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IRDB.Startup))]
namespace IRDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
