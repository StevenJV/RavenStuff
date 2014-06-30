using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RMDB.Startup))]
namespace RMDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
