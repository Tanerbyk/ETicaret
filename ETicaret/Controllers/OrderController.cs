using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Dal.Web;
using ETicaret.Web.Application.Basket;
using ETicaret.Web.Application.Features.Order.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        [HttpGet]
        public IActionResult CreateOrder() { 
            return View();
        }

        [HttpPost]
        public async Task<bool> CreateOrder(CreateOrderCommand c)
        {
            string a = _userManager.GetUserId(User);
            c.UserId = a;      
            var data = await _mediator.Send(c);
            return data;

        }
        public  Task<IActionResult> OrderList()
        {
            return View();

        }





    }
}
