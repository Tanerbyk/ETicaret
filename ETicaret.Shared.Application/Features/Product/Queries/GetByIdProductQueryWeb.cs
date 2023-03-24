using AutoMapper;
using ETicaret.Shared.Application.DTOs;
using ETicaret.Shared.Dal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Product.Queries
{
    public  class GetByIdProductQueryWeb : IRequest<ProductDto>
    {
        public int Id { get; set; }

        public class GetByIdProductQueryWebHandler : IRequestHandler<GetByIdProductQueryWeb, ProductDto>
        {
            private readonly MarketPlaceDbContext _db;
            private readonly IMapper _mapper;

            public GetByIdProductQueryWebHandler(MarketPlaceDbContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            public async Task<ProductDto> Handle(GetByIdProductQueryWeb request, CancellationToken cancellationToken)
            {
                var product =  _db.Products.FirstOrDefault(x=>x.Id == request.Id);

                var productDto =   _mapper.Map<ProductDto>(product);  

                return productDto;
            }
        }
    }
}
