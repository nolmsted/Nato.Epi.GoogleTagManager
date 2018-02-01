using System.Web.Mvc;
using System.Web.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using Nato.Epi.GoogleTagManager.App_Start;

namespace Nato.Epi.GoogleTagManager.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    internal class RouteInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
