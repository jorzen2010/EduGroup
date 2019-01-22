using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EduGroup.Startup))]
namespace EduGroup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
