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
        private readonly CookieOptions _cookieOptions;

        public CookieService(CookieOptions cookieOptions)
        {
            _cookieOptions = cookieOptions;
            
        }

        
    }

}
