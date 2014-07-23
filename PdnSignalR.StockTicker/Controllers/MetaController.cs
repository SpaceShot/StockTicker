using System.Web.Mvc;

namespace PdnSignalR.StockTicker.Controllers
{
    public class MetaController : Controller
    {
        //
        // GET: /Meta/

        public ActionResult Index()
        {
            ViewBag.Stocks = StockCore.StockTicker.Instance.GetAllStocks();

            return View();
        }

    }
}
