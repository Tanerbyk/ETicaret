using ETicaret.Shared.Dal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Category.Commands
{
    public class CreateCategoryCommand:IRequest<bool>
    {
        public string Name { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, bool>
        {
            private readonly MarketPlaceDbContext _db;
            public CreateCategoryCommandHandler(MarketPlaceDbContext db)
            {
                _db= db;
            }
            public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (!_db.Categories.Any(x => x.Name == request.Name))
                    {
                        var category = await _db.Categories.AddAsync(new Dal.Concrete.Category { Name = request.Name });
                        await _db.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {

                    return false;
                }
             }
        }


    }
 
}
