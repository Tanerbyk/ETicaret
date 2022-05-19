using ETicaret.Shared.Dal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Product.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
        {
            private readonly MarketPlaceDbContext _db;

            public DeleteProductCommandHandler(MarketPlaceDbContext db)
            {
                _db = db;
            }

            public  async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var data =  _db.Products.FirstOrDefault(x => x.Id == request.Id);
                _db.Products.Remove(data);
                 await _db.SaveChangesAsync();
                return true;
            }
        }
    }
}
