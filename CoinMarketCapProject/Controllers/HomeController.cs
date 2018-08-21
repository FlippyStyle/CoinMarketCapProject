using System.Web.Mvc;
using CoinMarketCapProject.Models;

namespace CoinMarketCapProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTable()
        {
            HttpHlp httpHlp = new HttpHlp();
            RootObject coins = httpHlp.GetCoins();
            return PartialView(coins);
        }
    }
}