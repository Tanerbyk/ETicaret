using ETicaret.Shared.Application.DTOs;
using MediatR;
using System.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Web;
using ETicaret.Shared.Dal.Concrete;

namespace ETicaret.Shared.Application.Features.Address.Queries
{
    public class GetUserAddressQuery : IRequest<AddressDto>
    {
        public string userid { get; set; }
        public class GetUserAddressQueryHandler : IRequestHandler<GetUserAddressQuery, AddressDto>
        {
            private readonly MarketPlaceDbContext _db;
            private readonly UserManager<WebUser> _userManager;

            public GetUserAddressQueryHandler(MarketPlaceDbContext db, UserManager<WebUser> userManager)
            {
                _db = db;
                _userManager = userManager;
            }

            public async Task<AddressDto> Handle(GetUserAddressQuery request, CancellationToken cancellationToken)
            {
                var userid = _userManager.GetUserId(User);

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
        }
    }
}
