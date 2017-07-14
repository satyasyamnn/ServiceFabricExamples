using System.Web.Http;

namespace WebCalculatorService
{
    public static class RouteConfig
    {
        public static void RegisterRouters(HttpRouteCollection routes)
        {
            routes.MapHttpRoute(
                name: "CalculatorApi",
                routeTemplate: "api/{action}",
                defaults: new { controller = "Default" }
            );
        }
    }
}
