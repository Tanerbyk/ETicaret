using ETicaret.Shared.Application.DTOs;
using ETicaret.Shared.Application.Features.Address.Queries;
using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.Web.Controllers
{
    public class AddressController : Controller
    {
        private readonly MarketPlaceDbContext _db;
        private readonly IMediator _mediator;

        public AddressController(IMediator mediator, MarketPlaceDbContext db)
        {
            _db = db;
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task< IActionResult> ListCity(string userid   )
        {
            var userAddress = await _db.Addresses.FirstOrDefaultAsync(x => x.UserId == userid);
             
            AddressDto ad = new AddressDto
            {
                Cities = await _db.Cities.ToListAsync(),
                Districts = await _db.Districts.Where(x=>x.CityId==userAddress.CityId).ToListAsync(),
                CityId = userAddress.CityId,
                DistrictId = userAddress.DistrictId,
                FullAddress = userAddress.FullAddress,
                AddressTitle = userAddress.AddressTitle,


            };

                      
            //ViewBag.City = new SelectList(_db.Cities, "CityId", "Name");           
            return View(ad);
         
        }
        [HttpPost]
        public   IActionResult ListCity(Address a,string userid)
        {
            var cid = _db.Addresses.FirstOrDefault(x => x.UserId == userid);
            cid.AddressDetail = a.AddressDetail;
            cid.CityId = a.CityId;
            cid.DistrictId = a.DistrictId;
            cid.AddressTitle = a.AddressTitle;
           

            _db.SaveChanges();
            return RedirectToAction("Index", "Product");

        }

        public async Task< IActionResult> CascadeList()
        {
            var data = await _mediator.Send(new GetAllCityQuery());
            return View(data);



        }
        public JsonResult LoadDistrict(int Id)
        {
            var districts = _db.Districts.Where(x=>x.CityId==Id).ToList();
            return Json(new SelectList(districts, "DistrictId", "CityName"));
        }



    }
}
