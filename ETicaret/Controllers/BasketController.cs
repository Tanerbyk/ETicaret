﻿using ETicaret.Web.Application.Basket;
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
        public async Task<string> AddBasketProduct(string userid,int productid,int quantity)

        {
            await _basketService.Add(userid, productid, quantity);
            return "success";
        }

        public async Task<IActionResult> GetAllProductBasket(string userid)
        {


           var values = await _basketService.Get(userid);
            return View(values);
        }

        public async Task<IActionResult> RemoveAllProductBasket(string userid)
        {

         
             await _basketService.RemoveAll(userid);
            return View();
        }
    }
}
