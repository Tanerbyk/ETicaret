using ETicaret.Shared.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Category.Queries
{
   public class GetCategoryByIdQuery : IRequest<Dal.Concrete.Category>
    {
        public int Id { get; set; } 

        
        


        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Dal.Concrete.Category>
        {
           private readonly  MarketPlaceDbContext _db;

            public GetCategoryByIdQueryHandler(MarketPlaceDbContext db)
            {
                _db = db;
            }

            public async Task<Dal.Concrete.Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
            {
                var data =  _db.Categories.Where(x => x.Id == request.Id ).FirstOrDefault();

                return data;                     

            }
        }


    }
}
