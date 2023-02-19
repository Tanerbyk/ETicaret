using ETicaret.Shared.Application.DTOs;
using ETicaret.Shared.Application.Features.Address.Queries;
using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using ETicaret.Web.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.Web.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        private readonly MarketPlaceDbContext _db;
        private readonly IMediator _mediator;
        private readonly UserManager<WebUser> _userManager;



        public AddressController(IMediator mediator, MarketPlaceDbContext db, UserManager<WebUser> userManager)
        {
            _db = db;
            _mediator = mediator;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Address(string userid)
        {
            var userAddress = await _db.Addresses.FirstOrDefaultAsync(x => x.UserId == userid);
            AddressDto ad = new();
            ad.Cities = await _db.Cities.ToListAsync();
            if (userAddress != null)
            {             
                ad.Districts = await _db.Districts.Where(x => x.CityId == userAddress.CityId).ToListAsync();
                ad.CityId = userAddress.CityId;
                ad.DistrictId = userAddress.DistrictId;
                ad.FullAddress = userAddress.FullAddress;
                ad.AddressTitle = userAddress.AddressTitle;
                ad.AdressDetail = userAddress.AddressDetail;

                //ViewBag.City = new SelectList(_db.Cities, "CityId", "Name");           
            }
            else
            {
                ad.Districts = await _db.Districts.Where(x => x.CityId == 1).ToListAsync();
                ad.CityId = 0;
                ad.DistrictId = 0;
                ad.FullAddress = "";
                ad.AddressTitle = "";
            }
            return View(ad);
        }

        [HttpPost]
        public IActionResult Address(Address a, string userid)
        {
            var cid = _db.Addresses.FirstOrDefault(x => x.UserId == userid);

            if (cid != null)
            {
                var city = _db.Cities.Include(x => x.District).FirstOrDefault(x => x.CityId == a.CityId);
                cid.AddressDetail = a.AddressDetail;
                cid.CityId = a.CityId;
                cid.DistrictId = a.DistrictId;
                cid.AddressTitle = a.AddressTitle;

                cid.FullAddress = $"{city.Name} {city.District.FirstOrDefault(x=>x.DistrictId==a.DistrictId).CityName} {a.AddressDetail}";

                _db.Addresses.Update(cid);
            }
            else
            {

                Address s = new Address();
                s.UserId = userid;
                s.AddressDetail = a.AddressDetail;
                s.CityId = a.CityId;
                s.DistrictId = a.DistrictId;
                s.AddressTitle = a.AddressTitle;
                DateTime dt = new DateTime();
                s.CreateDate = DateTime.Now;
                s.ModifiedDate = DateTime.Now;
                s.IsActive = true;
                s.FullAddress =$"{s.CityId} {s.DistrictId} {s.AddressDetail}";

                _db.Addresses.Add(s);


            }



            _db.SaveChanges();
            return RedirectToAction("Index", "Product");

        }

        public async Task<IActionResult> CascadeList()
        {
            var data = await _mediator.Send(new GetAllCityQuery());
            return View(data);
            
            

        }
        public JsonResult LoadDistrict(int Id)
        {
            var districts = _db.Districts.Where(x => x.CityId == Id).ToList();
            return Json(new SelectList(districts, "DistrictId", "CityName"));
        }



    }
}
