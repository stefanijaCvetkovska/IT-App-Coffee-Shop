using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IT_Mvc_App_Coffee_Shop.Startup))]
namespace IT_Mvc_App_Coffee_Shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
