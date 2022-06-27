using ETicaret.Web.Application.Basket;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ETicaret.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBasketProduct()
        {
            await _basketService.Add("8361bae0-9133-4417-9f33-672e784f9439", 2, 1);
            return View();
        }
    }
}
