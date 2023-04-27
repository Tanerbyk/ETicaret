using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Web.Application.Features.Order.Queries
{
    public class GetOrderByIdQuery : IRequest<Shared.Dal.Concrete.Order>
    {
        public int OrderId { get; set; }
        public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Shared.Dal.Concrete.Order>
        {
            private readonly MarketPlaceDbContext _db;

            public GetOrderByIdQueryHandler(MarketPlaceDbContext db)
            {
                _db = db;
            }

            public async Task<Shared.Dal.Concrete.Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var data =  _db.Orders.Include(x=>x.OrderDetails).ThenInclude(x=>x.Product).FirstOrDefault(x=>x.Id==request.OrderId);
                return data;
            }
        }
    }



}
