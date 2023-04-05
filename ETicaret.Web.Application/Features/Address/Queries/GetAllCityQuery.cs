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
    public class GetAllCityQuery :IRequest<List<City>>
    {

        public class GetAllCityQueryHandler : IRequestHandler<GetAllCityQuery, List<City>>
        {
            private readonly MarketPlaceDbContext _db;

            public GetAllCityQueryHandler(MarketPlaceDbContext db)
            {
                _db = db;
            }

            public async Task<List<City>> Handle(GetAllCityQuery request, CancellationToken cancellationToken)
            {
                var data = await _db.Cities.ToListAsync();
                return data;
            }
        }
    }
}
