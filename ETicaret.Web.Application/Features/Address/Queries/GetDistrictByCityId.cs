using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Address.Queries
{
    public class GetDistrictByCityId : IRequest<List<District>>
    {
        public int CityId { get; set; }

        public class GetDistrictByCityIdHandler : IRequestHandler<GetDistrictByCityId, List<District>>
        {
            private readonly MarketPlaceDbContext _db;

            public GetDistrictByCityIdHandler(MarketPlaceDbContext db)
            {
                _db = db;
            }

            public  Task<List<District>> Handle(GetDistrictByCityId request, CancellationToken cancellationToken)
            {
                var data = _db.Districts.Where(x=>x.CityId==request.CityId).ToListAsync();
                return data;
               
            }
        }
    }
}
