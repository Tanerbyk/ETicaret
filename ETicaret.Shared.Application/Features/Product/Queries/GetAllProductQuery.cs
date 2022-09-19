using ETicaret.Shared.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Product.Queries
{
    public class GetAllProductQuery : IRequest<List<Dal.Concrete.Product>>
    {
        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<Dal.Concrete.Product>>
        {
           private readonly MarketPlaceDbContext _db;

            public GetAllProductQueryHandler(MarketPlaceDbContext db)
            {
                _db = db;
            }

            public async  Task<List<Dal.Concrete.Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
            {
                
                var data = await _db.Products.Include(x=>x.Category).ToListAsync();
                //_db.Addresses.FirstOrDefault(x => x.city == "Ankara");
                return data;
            }
        }





    }
}
