using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETicaret.Management.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category c)
        {

            c.CreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.IsActive = true;
            _unitOfWork.Category.Add(c);
            _unitOfWork.Save();
            return View();
        }
        public IActionResult ListCategory()
        {           
            
                var values = _unitOfWork.Category.GetAll();
                
                return View(values);
              
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _unitOfWork.Category.Get(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category c)
        {
            c.ModifiedDate = DateTime.Parse(DateTime.Now.ToShortTimeString());
            _unitOfWork.Category.Update(c);
            _unitOfWork.Save();
            return RedirectToAction("ListCategory");

        }
        public IActionResult DeleteCategory(int id)
        {
            var c = _unitOfWork.Category.Get(id);
            _unitOfWork.Category.Remove(c);
            _unitOfWork.Save();
            return RedirectToAction("ListCategory");

        }

    }
}
