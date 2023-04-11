using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Web.Application.Features.Order.Commands
{
    public class CreateOrderCommand :  IRequest<bool>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
                  
        
        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
        {
            private readonly MarketPlaceDbContext _db;

            public CreateOrderCommandHandler(MarketPlaceDbContext db)
            {
                _db = db;
            }

            public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                await _db.Orders.AddAsync(new Shared.Dal.Concrete.Order { Id=request.Id,UserId=request.UserId,TotalPrice=request.TotalPrice,OrderDetails=request.OrderDetails});
                return true;
            }
        }
    }
}

