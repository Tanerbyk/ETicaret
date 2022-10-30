using Microsoft.AspNetCore.Mvc;

namespace ETicaret.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        } 
    }
}
