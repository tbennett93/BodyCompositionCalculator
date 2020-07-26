using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BodyCompositionCalculator.Startup))]
namespace BodyCompositionCalculator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
