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
        

        public OrderController(UserManager<WebUser> userManager, IMediator mediator, IBasketService basketService)
        {
            _userManager = userManager;
            _mediator = mediator;
           
        }

        [HttpPost]
        public IActionResult CreateOrder(CreateOrderCommand c)
        {
            string a = _userManager.GetUserId(User);
            c.UserId = a;      
            var data = _mediator.Send(c);
           return View(data);

        }





    }
}
