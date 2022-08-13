using ETicaret.Web.Application.Basket;
using ETicaret.Web.Application.DTOs;
using ETicaret.Web.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ETicaret.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly UserManager<WebUser> _userManager;


        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<BasketDTO> AddBasketProduct(string userid,int productid,int quantity)

        {
            await _basketService.Add(userid, productid, quantity);
            var data  = await _basketService.Get(userid);
            return data;
        }

        public async Task<IActionResult> GetAllProductBasket(string userid)
        {


           var values = await _basketService.Get(userid);
            return View(values);
        }

        public async Task<IActionResult> RemoveAllProductBasket(string userid)
        {

         
             await _basketService.RemoveAll(userid);
            return RedirectToAction("GetAllProductBasket");
        }

        public async Task<IActionResult> RemoveItemBasket(string userid,int ProductId)
        {


            await _basketService.Remove(userid,ProductId);
            return RedirectToAction("GetAllProductBasket");
        }
    }
}
