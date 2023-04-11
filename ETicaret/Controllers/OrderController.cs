using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Dal.Web;
using ETicaret.Web.Application.Basket;
using ETicaret.Web.Application.Features.Order.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<WebUser> _userManager;
        private readonly IMediator _mediator;
        private readonly IBasketService _basketService; 

        public OrderController(UserManager<WebUser> userManager, IMediator mediator, IBasketService basketService)
        {
            _userManager = userManager;
            _mediator = mediator;
            _basketService = basketService;
        }

        public IActionResult CreateOrder(CreateOrderCommand c)
        {
            string a = _userManager.GetUserId(User);
            c.UserId = a;
            var basket = _basketService.Get(a);
           foreach (var item in basket.Result.BasketProducts) {
 
                c.OrderDetails.Add(new OrderDetail { ProductId = item.ProductId, Quantity = item.Quantity });
            }
            var data = _mediator.Send(c);
           return View(data);

        }





    }
}
