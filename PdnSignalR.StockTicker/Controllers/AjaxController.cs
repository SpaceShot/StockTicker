using System.Web.Mvc;

namespace PdnSignalR.StockTicker.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/

        public ActionResult Index()
        {
            ViewBag.Stocks = StockCore.StockTicker.Instance.GetAllStocks();

            return View();
        }

    }
}
