using System.Web;
using System.Web.Mvc;

namespace PdnSignalR.StockTicker
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}