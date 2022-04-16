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
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ETicaret.Shared.Application.Extensions;
using Newtonsoft.Json;

namespace ETicaret.Management.Controllers
{
    public class ProductController : Controller
    {


        private readonly IMemoryCache _memoryCache;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _phsyicalPath;

        public ProductController(IUnitOfWork unitOfWork, IOptions<FilePathOptions> options, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _phsyicalPath = Path.Combine(options.Value.RootPath, options.Value.GetByKey(FileKeys.Products).Key);
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> ListProduct()
        {
            const string key = "Products";

            if (_memoryCache.TryGetValue(key, out object list))
            {
                return View(list);
            }
            else
            {
                List<Product> values = await SetProductOnMemory(key);

                return View(values);
            }

        }
        private async Task<List<Product>> SetProductOnMemory(string key = "Products")
        {
            var values = await _unitOfWork.Products.GetListWithCategory();

            _memoryCache.Set(key, values, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(20),
                Priority = CacheItemPriority.Normal
            });
            return values;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var values = await _unitOfWork.Category.GetAll();
            return View(values);


        }


        public ActionResult Test()
        {
            return Json("OK");
        }

        [HttpPost]
        //[ResponseCache(CacheProfileName = "Create")]

        public ActionResult Ekle(Product p, [FromForm] IFormFile file)
        {

            try
            {
                ProductValidator pv = new ProductValidator();
                FluentValidation.Results.ValidationResult result = pv.Validate(p);
                if (result.IsValid)
                {
                    if (file is not null)
                    {
                        var ex = Path.GetExtension(file.FileName);
                        var newname = Guid.NewGuid() + ex;
                        var filePath = Path.Combine(_phsyicalPath, newname);
                        p.Path = newname;


                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                    }
                    _unitOfWork.Products.Add(p);
                    _unitOfWork.Save();
                    //"spanErr_"+ item.PropertyName, "Ürün adı boş olamaz";
                    //"spanErr_Name", "Ürün adı boş olamaz";
                    //return Json(new Response { Data ="Ekleme işleminiz başarı ile tamamlandı.", Status = StatusCode.Success });

                    return Json(new Response { Data = "ListProduct", Status = StatusCode.Success });
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    string strErr = string.Join("<br/>", result.Errors.Select(x => x.ErrorMessage));

                    return Json(new Response { Data = result.Errors, Status = StatusCode.Error });

                }
            }
            catch (Exception ex)
            {

                return Json(new Response { Data = "test" , Status = StatusCode.Error });

            }


        }
        public class Response
        {
            public object Data { get; set; }
            public StatusCode Status { get; set; }
        }
        public enum StatusCode
        {

            Error,
            Success
        }
        public class SharedErrors
        {
            public string Property { get; set; }
            public string Message { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            List<SelectListItem> categoryvalues = (from x in await _unitOfWork.Category.GetAll()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Name,
                                                       Value = x.Id.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            var values = _unitOfWork.Products.Get(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product p)
        {

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
