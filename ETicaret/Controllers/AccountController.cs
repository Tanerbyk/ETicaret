using ETicaret.Shared.Dal;
using ETicaret.Shared.Data;
using ETicaret.Web.IdentityContext;
using ETicaret.Web.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.Web.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly UserManager<WebUser> _userManager;
        private readonly WebDbContext _context;
        private readonly SignInManager<WebUser> _signInManager;

        public AccountController( UserManager<WebUser> userManager)
        {      
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAccount(string id)
        {
            var value = await _userManager.FindByIdAsync(id);
            return View(value);
        }

        [HttpPost] 
        public async Task<IActionResult> UpdateAccount(WebUser webUser , string id )
        {
            var value = _userManager.Users.FirstOrDefault(x=>x.Id==id);
            
            value.FirstName = webUser.FirstName;
            value.LastName = webUser.LastName;
            value.Email = webUser.Email;
            //var token = await _userManager.GeneratePasswordResetTokenAsync(webUser);
            //var result = await _userManager.ResetPasswordAsync(webUser, token, value.newpassword);
            
            await _userManager.UpdateAsync(value);
            

            return RedirectToAction("Index","Product");

            
           
            
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }








    }
}
