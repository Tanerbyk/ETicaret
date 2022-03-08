using ETicaret.Model;
using ETicaret.Shared.Repository.UnitOfWork;
using ETicaret.Shared.Dal;
using System.Configuration;

namespace ETicaret.Controllers
{
    public class HomeController :Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public HomeController(IStringLocalizer<SharedResources> localizer, IUnitOfWork unitOfWork)


        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }
    public IActionResult Index()
        {
            //var TEST= _unitOfWork.Users.GetActiveUsers();
            //_unitOfWork.SaveAsync();
            //for (int i = 0; i < 10; i++)
            {
                //Ekleme Kodu
                //Adres Ekleme



            }
            // _methods.SliderAdd(slider);
            //_unitOfWork.SaveAsync();
            ////ViewData["Message"] = _localizer["Message"];
            //ViewData["Merhaba"] = _localizer.GetString("Merhaba");
            return View();
        }
    }
}
