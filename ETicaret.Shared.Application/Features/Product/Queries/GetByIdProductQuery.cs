
using ETicaret.Shared.Dal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Product.Queries
{
    public class GetByIdProductQuery : IRequest<Dal.Concrete.Product>
    {
        public int Id { get; set; }
        public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, Dal.Concrete.Product>
        {
            private readonly MarketPlaceDbContext _db;

            public GetByIdProductQueryHandler(MarketPlaceDbContext db)
            {
                _db = db;
            }

            public async Task<Dal.Concrete.Product> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
            {
                var data = _db.Products.FirstOrDefault(x => x.Id == request.Id);
                return data;
            }
        }
    }
}
