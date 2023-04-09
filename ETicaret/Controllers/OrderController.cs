using ETicaret.Shared.Dal.Web;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<WebUser> _userManager;
        private readonly Mediator _mediator;

        public OrderController(UserManager<WebUser> userManager, Mediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public IActionResult CreateOrder()
        {
            string a = _userManager.GetUserId(User);


        }





    }
}
