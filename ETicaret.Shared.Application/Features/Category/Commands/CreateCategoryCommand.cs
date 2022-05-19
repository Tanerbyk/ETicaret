using ETicaret.Shared.Dal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Category.Commands
{
    public class CreateCategoryCommand:IRequest<string>
    {
        public string Name { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, string>
        {
            private readonly MarketPlaceDbContext _db;
            public CreateCategoryCommandHandler(MarketPlaceDbContext db)
            {
                _db= db;
            }
            public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (!_db.Categories.Any(x => x.Name == request.Name))
                    {
                        var category = await _db.Categories.AddAsync(new Dal.Concrete.Category { Name = request.Name });
                        await _db.SaveChangesAsync();
                        return "success";
                    }
                    else
                    {
                        return "error";
                    }
                }
                catch (Exception ex)
                {

                    return "Kategori adı boş bırakılamaz." + ex.ToString();
                }
             }
        }


    }
 
}
