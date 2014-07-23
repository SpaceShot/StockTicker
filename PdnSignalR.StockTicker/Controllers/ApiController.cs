using System.Web.Mvc;

namespace PdnSignalR.StockTicker.Controllers
{
    public class ApiController : Controller
    {
        //
        // POST: /Api/
        [HttpPost]
        public JsonResult Index()
        {
            return Json(StockCore.StockTicker.Instance.GetAllStocks());
        }
    }
}
