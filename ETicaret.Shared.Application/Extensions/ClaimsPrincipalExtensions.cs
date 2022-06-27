using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal identity)
        {
            if (identity == null)
                return null;

            var first = identity.FindFirst(ClaimTypes.NameIdentifier);
            return first?.Value;
        }
        public static string GetUserName(this ClaimsPrincipal identity)
        {
            if (identity == null)
                return null;

            var first = identity.FindFirst(ClaimTypes.Name)?.Value;
            return $"{first}".Trim();
        }
        public static string GetUserNameSurName(this ClaimsPrincipal identity)
        {
            if (identity == null)
                return null;

            var first = identity.FindFirst(ClaimTypes.GivenName);
            var last = identity.FindFirst(ClaimTypes.Surname);
            return $"{first} {last}".Trim();
        }
        public static string GetUserEmail(this ClaimsPrincipal identity)
        {
            if (identity == null)
                throw new Exception("Değer null idi.");

            var first = identity.FindFirst(ClaimTypes.Email);
            return first?.Value;
        }
        //public static int GetVendorId(this ClaimsPrincipal identity)
        //{
        //    var first = identity.Claims.FirstOrDefault(x => x.Value != "" && x.Type == VendorCacheKeys.VendorId);
        //    return Convert.ToInt32(first?.Value);
        //}
    }
}
