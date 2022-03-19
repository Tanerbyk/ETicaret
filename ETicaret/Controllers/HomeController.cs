using Microsoft.AspNetCore.Mvc;

namespace ETicaret.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
