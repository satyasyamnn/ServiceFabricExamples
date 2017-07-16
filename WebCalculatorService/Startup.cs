using Owin;
using System.Web.Http;

namespace WebCalculatorService
{
    public class Startup : IOwinAppBuilder
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            FormatterConfig.ConfigureFormatters(config.Formatters);
            RouteConfig.RegisterRouters(config.Routes);
            appBuilder.UseWebApi(config);
        }
    }
}
