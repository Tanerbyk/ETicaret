using ETicaret.Web.Application.Basket;
using System.Threading.Tasks;
using ETicaret.Shared.Application.Extensions;
using System.Security.Claims;

namespace ETicaret.Web.Views.Shared.Components.Basket
{
    public class BasketComponent : ViewComponent
    {
        private readonly IBasketService _basketService;

        public BasketComponent(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            string userid = GetUserId(UserClaimsPrincipal);
            var values = await _basketService.Get(userid);

            return View(values);


        }
        private string GetUserId(ClaimsPrincipal identity)
        {
            if (identity == null)
                return null;
            
            var first = identity.FindFirst(ClaimTypes.NameIdentifier);
            return first?.Value;
        }
    }
}
