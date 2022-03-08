using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Repository.EntityFramework;
using ETicaret.Shared.Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETicaret.Management.Controllers
{
    public class ProductController : Controller
    {


     
        private readonly IUnitOfWork _unitOfWork;
       
        
        public ProductController(IUnitOfWork unitOfWork)
        {           
            _unitOfWork = unitOfWork;
        }

        public IActionResult ListProduct()
        {
            var values = _unitOfWork.Products.GetListWithCategory();
            return View(values);
        }
        [HttpGet]
        public IActionResult Create()
        {         
            List<SelectListItem> categoryvalues = (from x in _unitOfWork.Category.GetAll()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Name,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;

            return View();
        }
        [HttpPost]
        public IActionResult Create(Product p)
        {
           
           
          
            
          if(p.Image != null)
            {
                var ex = Path.GetExtension(p.Image.FileName);
                var newname  = Guid.NewGuid() + ex;
                var loc = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Image/", newname);
                var stream = new FileStream(loc, FileMode.Create);
                p.Image.CopyTo(stream);
                
            }

            _unitOfWork.Products.Add(p);
            
            _unitOfWork.Save();
            return RedirectToAction("Index","Home");
            
        }
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            List<SelectListItem> categoryvalues = (from x in _unitOfWork.Category.GetAll()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Name,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            var values = _unitOfWork.Products.Get(id);     
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product p )
        {
            p.ModifiedDate = DateTime.Parse(DateTime.Now.ToShortTimeString());
            _unitOfWork.Products.Update(p);
            _unitOfWork.Save();
            return RedirectToAction("ListProduct");
        }

        public IActionResult ProductDetails(int id)
        {
            var value = _unitOfWork.Products.Get(id);
            return View(value);
        }

        public IActionResult DeleteProduct(int id)
        {         
            var values = _unitOfWork.Products.Get(id);
            _unitOfWork.Products.Remove(values);
            _unitOfWork.Save();
            return RedirectToAction("ListProduct");
        }




        //[HttpPost]
        //public async Task<int> Create(Product model, [FromForm] IFormFile file)
        //{


        //    string folderPath = "/site/images/";
        //    string fileNameGuid = Guid.NewGuid().ToString();
        //    var fileNameAndExt = fileNameGuid + "_" + file.FileName + Path.GetExtension(file.FileName);

        //    model.Image = folderPath + fileNameAndExt;


        //    var filePath = Path.Combine(folderPath, fileNameAndExt);
        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        file.CopyTo(fileStream);
        //    }

        //    _unitOfWork.Products.Add(model);

        //    return await _unitOfWork.SaveAsync();
        //}
    }
}
