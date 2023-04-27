using ETicaret.Shared.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Web.Application.Features.Order.Queries
{
    public class GetAllOrdersQuery : IRequest<List<Shared.Dal.Concrete.Order>>
    {
        public string UserId { get; set; }
        public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<Shared.Dal.Concrete.Order>>
        {
            private readonly MarketPlaceDbContext _db;

            public GetAllOrdersQueryHandler(MarketPlaceDbContext db)
            {
                _db = db;
            }

            public async Task<List<Shared.Dal.Concrete.Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
            {
                var data = await _db.Orders.Where(x => x.UserId == request.UserId).Include(x=>x.OrderDetails).ToListAsync();
                return data;
            }
        }
    }
}
