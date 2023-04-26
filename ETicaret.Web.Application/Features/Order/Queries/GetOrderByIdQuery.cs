using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Web.Application.Features.Order.Queries
{
    public class GetOrderByIdQuery : IRequest<List<OrderDetail>>
    {
        public int OrderId { get; set; }
        public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, List<OrderDetail>>
        {
            private readonly MarketPlaceDbContext _db;

            public GetOrderByIdQueryHandler(MarketPlaceDbContext db)
            {
                _db = db;
            }

            public async Task<List<OrderDetail>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var data = _db.OrderDetails.Where(x => x.OrderId == request.OrderId);
                return data.ToList();
            }
        }
    }



}
