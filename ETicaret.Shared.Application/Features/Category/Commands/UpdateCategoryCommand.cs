using ETicaret.Shared.Dal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Category.Commands
{
    public class UpdateCategoryCommand : IRequest<string>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; } = true;
        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand,string>
        {
            private readonly MarketPlaceDbContext _db;

            public UpdateCategoryCommandHandler(MarketPlaceDbContext db)
            {
                _db = db;
            }

            public async Task<string> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = _db.Categories.Where(a => a.Id == request.Id).FirstOrDefault();
                if(category != null)
                {
                    category.Name = request.Name;
                    category.Id = request.Id;
                    category.IsActive = request.IsActive;
                    await _db.SaveChangesAsync();

                    return "success";
                }
                else
                {
                    return "error";
                }
                

            }
        }
    }
}
