using ETicaret.Shared.Application.DTOs;
using ETicaret.Shared.Application.Features.Address.Commands;
using ETicaret.Shared.Application.Features.Address.Queries;
using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Dal.Web;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nest;
using NuGet.Protocol;
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

        public async Task<IActionResult> Address( )
        {
          var userid=   _userManager.GetUserId(User);
           var data =    await _mediator.Send(new GetUserAddressQuery { userid = userid });
            return View(data);
        }
        
        [HttpPost]
        public IActionResult Address(UpdateAddressCommand a)
        {
            var data = _mediator.Send(a);
            return RedirectToAction("Address", "Address");

        }

        public async Task<JsonResult> LoadDistrict(int Id)
        {
            var districts = await _mediator.Send(new GetDistrictByCityId { CityId = Id });

            return Json(new SelectList(districts, "DistrictId", "CityName"));
        }



    }
}
