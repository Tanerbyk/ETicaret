using ETicaret.Shared.Application.Features.Account.Commands;
using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Web;
using ETicaret.Shared.Data;
using ETicaret.Web.IdentityContext;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;


namespace ETicaret.Web.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly UserManager<WebUser> _userManager;
        private readonly IMediator _mediator;

        public AccountController( UserManager<WebUser> userManager, IMediator mediator)
        {      
            _userManager = userManager;
            _mediator = mediator;
        } 
 
        [HttpGet]
        public  IActionResult UpdateAccount()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> UpdateAccount(WebUser webUser ,UpdateAccountCommand account)
        {
            account.UserId = _userManager.GetUserId(User);
            var data = await _mediator.Send(account);
            return RedirectToAction("UpdateAccount", "Account");                      
        }
     







    }
}
