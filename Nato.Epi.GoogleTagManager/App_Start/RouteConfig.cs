using System.Web.Mvc;
using System.Web.Routing;

namespace Nato.Epi.GoogleTagManager.App_Start
{
    internal class RouteConfig
    {
        internal static void RegisterRoutes(RouteCollection routes)
        {                       
            routes.MapRoute(
                name: "GoogleTagManagerAdmin",
                url: "modules/Nato.Epi.GoogleTagManager/{controller}/{action}",
                defaults: new { controller = "GoogleTagManagerAdmin", action = "Index" },
                constraints: new { controller = "GoogleTagManagerAdmin" }
                );
        }
    }
}
