using ETicaret.Shared.Dal;
using ETicaret.Web.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utf8Json;

namespace ETicaret.Web.Application.Features.Basket.Commands
{
    public class AddToBasketRedisCommand : IRequest<string>
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public class AddToBasketRedisCommandHandler : IRequestHandler<AddToBasketRedisCommand, string>
        {
            private readonly MarketPlaceDbContext _db;
            private readonly IDistributedCache _distributedCache;

            public AddToBasketRedisCommandHandler(MarketPlaceDbContext db, IDistributedCache distributedCache)
            {
                _db = db;
                _distributedCache = distributedCache;
            }

            public async Task<string> Handle(AddToBasketRedisCommand request, CancellationToken cancellationToken)
            {
                var redisData = _distributedCache.GetString("basket_" + request.UserId);
                BasketDTO redisBasket = new BasketDTO();
                if (redisData is not null)
                {
                    redisBasket = Utf8Json.JsonSerializer.Deserialize<BasketDTO>(redisData);
                }
                var product = await _db.Products 
                    
                    .FirstOrDefaultAsync(x => x.Id == request.ProductId);

                if (!redisBasket.BasketProducts.Any())//Count()==0
                {
                    BasketDTO basketDTO = new BasketDTO
                    {
                        UserId = request.UserId,
                        BasketProducts = new List<BasketProducts>
                        {
                             new BasketProducts  {
                            ProductId = request.ProductId,
                            Quantity = request.Quantity,
                            ProductName = product.Name,
                            Path = product.Path,

                            ProductPrice = product.Price,

                             }
                        }

                    };
                    _distributedCache.SetString("basket_" + request.UserId, JsonSerializer.ToJsonString(basketDTO));
                }
                else
                {
                    var redisProduct = redisBasket.BasketProducts.FirstOrDefault(x =>
                        x.ProductId == request.ProductId); //gelen ürün sepette var mı

                    if (redisProduct is null)
                    {
                        BasketProducts currentProduct = new BasketProducts
                        {
                            ProductId = request.ProductId,
                            Quantity = request.Quantity,
                            ProductName = product.Name,
                            Path = product.Path,
                            ProductPrice = product.Price,


                        };
                        redisBasket.BasketProducts.Add(currentProduct);

                    }
                    else
                    {
                        int currentQuantity = redisProduct.Quantity + request.Quantity;

                        redisProduct.ProductPrice = product.Price;
                        redisProduct.Quantity = currentQuantity;
                    }
                    _distributedCache.SetString("basket_" + request.UserId, JsonSerializer.ToJsonString(redisBasket));


                }
                var generalBasket = await _distributedCache.GetStringAsync("general_Basket", token: cancellationToken);
                return "success";
            }
        }
    }
}