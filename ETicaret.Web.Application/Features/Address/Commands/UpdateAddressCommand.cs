using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Address.Commands
{
    public class UpdateAddressCommand : IRequest<bool>
    {
        public int AddressId { get; set; }


        public string AddressTitle { get; set; }
        public string FullAddress { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }

        public string UserId { get; set; }

        public string AddressDetail { get; set; }
        public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, bool>
        {
            private readonly MarketPlaceDbContext _db;

            public UpdateAddressCommandHandler(MarketPlaceDbContext db)
            {
                _db = db;
            }

            public async Task<bool> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
            {
                var cid = _db.Addresses.FirstOrDefault(x => x.UserId == request.UserId);

                if (cid != null)
                {
                    var city = _db.Cities.Include(x => x.District).FirstOrDefault(x => x.CityId == request.CityId);
                    cid.AddressDetail = request.AddressDetail;
                    cid.CityId = request.CityId;
                    cid.DistrictId = request.DistrictId;
                    cid.AddressTitle = request.AddressTitle;

                    cid.FullAddress = $"{city.Name} {city.District.FirstOrDefault(x => x.DistrictId == request.DistrictId).CityName} {request.AddressDetail}";

                    _db.Addresses.Update(cid);
                }
                else
                {

                    Dal.Concrete.Address s = new ();
                    s.UserId = request.UserId;
                    s.AddressDetail = request.AddressDetail;
                    s.CityId = request.CityId;
                    s.DistrictId = request.DistrictId;
                    s.AddressTitle = request.AddressTitle;
                    DateTime dt = new DateTime();
                    s.CreateDate = DateTime.Now;
                    s.ModifiedDate = DateTime.Now;
                    s.IsActive = true;
                    s.FullAddress = $"{s.CityId} {s.DistrictId} {s.AddressDetail}";

                    _db.Addresses.Add(s);


                }
                _db.SaveChanges();

                return true;
            }
        }
    }
}
