using Microsoft.AspNetCore.Identity;

namespace ETicaret.Web.IdentityModel
{
    public class WebUser:IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }
    }
}
