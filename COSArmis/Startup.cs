using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentSearch.Startup))]
namespace StudentSearch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
