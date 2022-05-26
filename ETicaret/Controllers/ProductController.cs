using AutoMapper;
using ETicaret.Shared.Application.DTOs;
using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient.Memcached;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly MarketPlaceDbContext _db;
       private readonly IMapper _mapper;


        public ProductController( IMapper mapper, MarketPlaceDbContext db)
        {
            _db = db;
            _mapper = mapper;

        }

        public async Task<IActionResult> Index()
        {

            var x = _db.Products.ToList();
            var productDto = _mapper.Map<List<ProductDto>>(x);

            return View(productDto);

        }

        public IActionResult ProductDetail(int id)
        {

            var values = _db.Products.FirstOrDefault(x=>x.Id==id);
            var productDto = _mapper.Map<ProductDto>(values);
            return View(productDto);

        }
    }
}
