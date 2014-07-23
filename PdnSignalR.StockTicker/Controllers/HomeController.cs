using System.Web.Mvc;

namespace PdnSignalR.StockTicker.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Stocks = StockCore.StockTicker.Instance.GetAllStocks();

            return View();
        }

    }
}
