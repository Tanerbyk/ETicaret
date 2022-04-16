using ETicaret.Shared.Application.Enums;
using ETicaret.Shared.Application.Extensions;
using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ETicaret.Management.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _distributedCache;

        public CategoryController(IUnitOfWork unitOfWork, IDistributedCache distributedCache)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
        }


        [HttpGet]

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category c)
        {

            _unitOfWork.Category.Add(c);
            _unitOfWork.Save();
            return View();
        }
        public async Task<IActionResult> ListCategory()
        {
            string key = CacheKeys.CategoryList.GetDisplayName();
 
            string cache = _distributedCache.GetString(key);

            if (cache is not null)
            {
                 var  value  = Utf8Json.JsonSerializer.Deserialize<List<Category>>(cache.ToString());
                return View(value);
            }
            else
            {
                var values = await _unitOfWork.Category.GetAll();
                _distributedCache.SetString(key, Utf8Json.JsonSerializer.ToJsonString(values));

                return View(values);

            }

        }

    

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            string key = CacheKeys.Category.GetDisplayName();

            var cache = _distributedCache.GetString(key);

            if (cache is not null)
            {
                Category category = Utf8Json.JsonSerializer.Deserialize<Category>(cache);
                return View(category);
            }
            else
            {
                var value = _unitOfWork.Category.Get(id);
                _distributedCache.SetString(key, Utf8Json.JsonSerializer.ToJsonString(value));
                return View(value);
            }

        }

        [HttpPost]
        public IActionResult UpdateCategory(Category c)
        {

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
