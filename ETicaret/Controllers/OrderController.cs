using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Dal.Web;
using ETicaret.Web.Application.Basket;
using ETicaret.Web.Application.Features.Order.Commands;
using ETicaret.Web.Application.Features.Order.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ETicaret.Web.Controllers
{
	[Authorize]
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
            string userid = _userManager.GetUserId(User);
            c.UserId = userid;
            var data = await _mediator.Send(c);
            return data;

        }
        [HttpGet]
        public async Task<IActionResult> OrderList(GetAllOrdersQuery getAllOrdersQuery)
        {
            var userid = _userManager.GetUserId(User);
            var data = await _mediator.Send(new GetAllOrdersQuery() { UserId = userid });
            return View(data);
        }

        public async Task<IActionResult> OrderComplete()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> OrderGetById(int Orderid)
        
        
        {
            var data = await _mediator.Send(new GetOrderByIdQuery { OrderId = 6 });
            return View(data);
        }

                              



    }
}
