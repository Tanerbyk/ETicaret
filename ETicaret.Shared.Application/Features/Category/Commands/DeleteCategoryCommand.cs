using ETicaret.Shared.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Category.Commands
{
    public class DeleteCategoryCommand : IRequest<string>
    {
        public int Id { get; set; }




        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, string>
        {
            private readonly MarketPlaceDbContext _db;
            public DeleteCategoryCommandHandler(MarketPlaceDbContext db)
            {
                _db = db;
            }

            public async Task<string> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var data = await _db.Categories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (data != null)
                {
                    _db.Categories.Remove(data);
                    await _db.SaveChangesAsync();
                    return request.Id + "Idli kategori silinmiştir.";

                }
                else
                {
                    return request.Id + "Bu Idye ait kategori bulunamamıştır.";
                }
                

            }
        }
    }
}
