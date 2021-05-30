using System.Web.Mvc;

namespace EcoSendWeb.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("about")]
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [Route("contact")]
        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }
    }
}