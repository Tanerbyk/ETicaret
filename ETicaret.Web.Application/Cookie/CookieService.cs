using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Web.Application.Cookie
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            
        }

        public void SetCookie(string key,string value)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(7);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key,value,option);
        }

        public string GetCookie(string key)
        {
          var values =   _httpContextAccessor.HttpContext.Request.Cookies[key];
            return values;
        }

        public void RemoveCookie(string key)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
            
        }







    }

}
