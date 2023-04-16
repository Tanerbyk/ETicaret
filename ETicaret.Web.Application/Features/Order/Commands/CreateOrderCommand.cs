using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Dal.Web;
using ETicaret.Web.Application.Basket;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Web.Application.Features.Order.Commands
{
    public class CreateOrderCommand : IRequest<bool>
    {

        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }


        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
        {
            private readonly MarketPlaceDbContext _db;
            private readonly IBasketService _basketService;
            private readonly UserManager<WebUser> _userManager;

            public CreateOrderCommandHandler(MarketPlaceDbContext db, IBasketService basketService)
            {
                _db = db;
                _basketService = basketService;
            }

            public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var basket = _basketService.Get(request.UserId);
                List<OrderDetail> ord = new();
                foreach (var item in basket.Result.BasketProducts)
                {
                    OrderDetail or = new();
                    or.ProductId = item.ProductId;
                    or.Quantity = item.Quantity;
                    ord.Add(or);
                }

                Shared.Dal.Concrete.Order order = new Shared.Dal.Concrete.Order()
                {
                    OrderDetails = ord,
                    UserId = request.UserId,
                    OrderDate = DateTime.Now,
                    TotalPrice = basket.Result.SubTotal

                };
                await _db.Orders.AddAsync(order);
                await _db.SaveChangesAsync();

                return true;
            }
        }
    }
}

