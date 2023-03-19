using AutoMapper;
using ETicaret.Shared.Application.DTOs;
using ETicaret.Shared.Application.Enums;
using ETicaret.Shared.Application.Extensions;
using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace ETicaret.Shared.Application.Features.Category.Queries
{
    //List<Category>

    public class GetAllCategoryQuery : IRequest<List<CategoryDTO>>
    {   
 
        public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<CategoryDTO>>
        {
            private readonly MarketPlaceDbContext _db;
            private readonly IMapper _mapper;

            public GetAllCategoryQueryHandler(MarketPlaceDbContext db, IMapper mapper)
            {
                _db = db;        
                _mapper = mapper;
            }
            public async Task<List<CategoryDTO>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
            {

                var categories =  await _db.Categories.ToListAsync();
                var categoryDTO =  _mapper.Map<List<CategoryDTO>>(categories);
                return categoryDTO;
                          

            }
        }

    }
}
