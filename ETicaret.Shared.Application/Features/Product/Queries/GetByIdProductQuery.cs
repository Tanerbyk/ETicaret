
using AutoMapper;
using ETicaret.Shared.Application.DTOs;
using ETicaret.Shared.Application.Features.Product.Commands;
using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Product.Queries
{
    public class GetByIdProductQuery : IRequest<UProductDto>
    {
        public int Id { get; set; }
        public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, UProductDto>
        {
            private readonly MarketPlaceDbContext _db;
            private readonly IMapper _mapper;

            public GetByIdProductQueryHandler(MarketPlaceDbContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
                
            }

            public async Task<UProductDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
            {

                var data = _db.Products.FirstOrDefault(x => x.Id == request.Id);
                var product = _mapper.Map<UProductDto>(data);
                return product;
            }
        }
    }
}
