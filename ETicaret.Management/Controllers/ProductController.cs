using ETicaret.Shared.Application;
using ETicaret.Shared.BusinessLayer.Validators;
using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Repository.UnitOfWork;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace ETicaret.Management.Controllers
{
    public class ProductController : Controller
    {



        private readonly IUnitOfWork _unitOfWork;
        private readonly string _phsyicalPath;

        public ProductController(IUnitOfWork unitOfWork, IOptions<FilePathOptions> options)
        {
            _unitOfWork = unitOfWork;
            _phsyicalPath = Path.Combine(options.Value.RootPath, options.Value.GetByKey(FileKeys.Products).Key);
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
        public IActionResult Create(Product p, [FromForm] IFormFile file)
        {

            ProductValidator pv = new ProductValidator();
            ValidationResult result = pv.Validate(p);

            if (result.IsValid)
            {
                if (file is not null)
                { 
                    var ex = Path.GetExtension(file.FileName);
                    var newname = Guid.NewGuid() + ex;
                    var filePath = Path.Combine(_phsyicalPath, newname); 
                    p.Path = newname;

                    using (var fileStream = new FileStream(filePath,FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                 

                }

                _unitOfWork.Products.Add(p);
                _unitOfWork.Save();
                return RedirectToAction("Create", "Product");

            }
            else
            {

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }
            }
            return View();

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
        public IActionResult UpdateProduct(Product p)
        {
            p.ModifiedDate = DateTime.Parse(DateTime.Now.ToShortTimeString());
            _unitOfWork.Products.Update(p);
            _unitOfWork.Save();
            return RedirectToAction("ListProduct");
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
