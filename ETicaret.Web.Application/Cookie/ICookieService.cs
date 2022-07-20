
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
        public void SetCookie(string key, string value);

        public string GetCookie(string key);

        public void RemoveCookie(string key);




    }
}
