using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ETicaret.Shared.Components
{
    public class MainSliderViewComponent : ViewComponent
    {
        public MainSliderViewComponent()
        {

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.CompletedTask;
            return View();
        }
    }
}
