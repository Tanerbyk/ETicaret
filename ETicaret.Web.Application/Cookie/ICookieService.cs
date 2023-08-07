
using ETicaret.Web.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Web.Application.Cookie
{
    public interface ICookieService
    {
         void SetCookie(string key, string value);

         string GetCookie(string key);

         void RemoveCookie(string key);




    }
}
