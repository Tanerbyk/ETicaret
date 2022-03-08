using ETicaret.Shared.Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.Web.Controllers
{
    public class ProductController : Controller
    {

        IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

         var values =  _unitOfWork.Products.GetAll();
            return View(values);

        }
    }
}
