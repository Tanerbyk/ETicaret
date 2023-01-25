using ETicaret.Web.Application.DTOs;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Web.Application.Features.Basket.Commands
{
    public class RemoveItemBasketRedisCommand : IRequest<bool>
    {
        public string UserId { get; set; }

        public int ProductId { get; set; }

        public class RemoveItemBasketRedisCommandHandler : IRequestHandler<RemoveItemBasketRedisCommand, bool>
        {
            private readonly IDistributedCache _distributedCache;

            public RemoveItemBasketRedisCommandHandler(IDistributedCache distributedCache)
            {
                _distributedCache = distributedCache;
            }

            public async Task<bool> Handle(RemoveItemBasketRedisCommand request, CancellationToken cancellationToken)
            {
                var cachemodel =  _distributedCache.GetString("basket_" + request.UserId);

                BasketDTO basketDTO = new BasketDTO();

                if(cachemodel is not null)
                {
                    basketDTO = Utf8Json.JsonSerializer.Deserialize<BasketDTO>(cachemodel);
                    var deletedItem = basketDTO.BasketProducts.FirstOrDefault(x => x.ProductId == request.ProductId);
                    basketDTO.BasketProducts.Remove(deletedItem);                
                    _distributedCache.SetString("basket_" + request.UserId,Utf8Json.JsonSerializer.ToJsonString(basketDTO));
                    return true;
                   
                }
                else
                {
                    return false;
                }
                

                
            }
        }
    }
}
