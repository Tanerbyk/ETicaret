using ETicaret.Shared.Application.Extensions;
using ETicaret.Web.Application.Basket;
using ETicaret.Web.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ETicaret.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService; 

        public BasketController(IBasketService basketService )
        {
            _basketService = basketService;
        }

        [HttpPost]
        public async Task<BasketDTO> AddBasketProduct(string userid, int productid, int quantity)

        {
            await _basketService.Add(userid, productid, quantity);
            var data = await _basketService.Get(userid);
            return data;
        }

        public async Task<IActionResult> GetAllProductBasket()
        {

            var values = await _basketService.Get(User.GetUserId());
            if (values.BasketProducts.Count > 0)
            {
                return View(values);

            }
            else
            {
                return RedirectToAction("EmptyBasket");
            }
        }
        public  IActionResult EmptyBasket()
        {
            
             return View();
         }

        public async Task<IActionResult> RemoveAllProductBasket(string userid)
        {


            await _basketService.RemoveAll(userid);
            return RedirectToAction("GetAllProductBasket");
        }

        public async Task<IActionResult> RemoveItemBasket(string userid, int ProductId)
        {


            await _basketService.Remove(userid, ProductId);
            return RedirectToAction("GetAllProductBasket");
        }

       
    }
}
