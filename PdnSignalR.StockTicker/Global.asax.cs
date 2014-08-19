using Microsoft.AspNet.SignalR;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace SpaceShot.Samples.StockTicker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<StockHub>();
            var listener = new StockBroadcaster(StockCore.StockTicker.Instance, hubContext);
 
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}