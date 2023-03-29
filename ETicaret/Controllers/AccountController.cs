using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Web;
using ETicaret.Shared.Data;
using ETicaret.Web.IdentityContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;


namespace ETicaret.Web.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly UserManager<WebUser> _userManager;

        public AccountController( UserManager<WebUser> userManager)
        {      
            _userManager = userManager;

        } 

        public IActionResult Index()
        {
           
            return View();
        }

        [HttpGet]
        public  IActionResult UpdateAccount()
        {
           
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> UpdateAccount(WebUser webUser )
        {
            var id = _userManager.GetUserId(User);
            var value = _userManager.Users.FirstOrDefault(x=>x.Id==id);
        
            value.FirstName = webUser.FirstName;
            value.LastName = webUser.LastName;
            value.Email = webUser.Email;
            
            await _userManager.UpdateAsync(value);
            

            return RedirectToAction("UpdateAccount", "Account");               
            
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }








    }
}
