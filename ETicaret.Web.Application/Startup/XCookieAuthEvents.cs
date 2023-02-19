using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ETicaret.Shared.Dal.Concrete;

namespace ETicaret.Web.Application.Startup
{
    public class XCookieAuthEvents : CookieAuthenticationEvents
    {
   
     
        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {

            context.RedirectUri = $"/Identity/Account/Login";
            return base.RedirectToLogin(context);
        }

        //public override Task RedirectToLogout(RedirectContext<CookieAuthenticationOptions> context)
        //{
        //    context.RedirectUri = $"/Identity/Account/CustomLogout";
        //    return base.RedirectToLogout(context);
        //}

        //public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
        //{
        //    context.RedirectUri = $"/Identity/Account/CustomAccessDenied";
        //    return base.RedirectToAccessDenied(context);
        //}

        //public override Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context)
        //{
        //    context.RedirectUri = $"/CustomReturnUrl";
        //    return base.RedirectToReturnUrl(context);
        //}
    }
}
