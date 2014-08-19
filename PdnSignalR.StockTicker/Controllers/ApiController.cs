using System.Web.Mvc;

namespace SpaceShot.Samples.StockTicker.Controllers
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
