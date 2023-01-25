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

        public class GetDistrictByCityIdHandler : IRequestHandler<GetDistrictByCityId, List<District>>
        {
            private readonly MarketPlaceDbContext _db;
            public async Task<List<District>> Handle(GetDistrictByCityId request, CancellationToken cancellationToken)
            {
                var data =await _db.Districts.ToListAsync();
                return data;
               
            }
        }
    }
}
