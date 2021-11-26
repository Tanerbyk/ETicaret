using ETicaret.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ETicaret.Controllers
{
    public class HomeController : Controller
    {

        private readonly IStringLocalizer<SharedResources> _localizer;
        public HomeController(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            ////ViewData["Message"] = _localizer["Message"];
            //ViewData["Merhaba"] = _localizer.GetString("Merhaba");
            return View();
        }
    }
}
