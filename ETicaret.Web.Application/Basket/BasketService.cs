﻿using ETicaret.Shared.Dal;
using ETicaret.Web.Application.Cookie;
using ETicaret.Web.Application.DTOs;
using ETicaret.Web.Application.Features.Basket.Commands;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace ETicaret.Web.Application.Basket
{
    public class BasketService : IBasketService
    {

        private readonly ICookieService _cookieService;

        private readonly IDistributedCache _distributedCache;
        private readonly IMediator _mediator;
        private readonly MarketPlaceDbContext _db;


        public BasketService(IDistributedCache distributedCache, IMediator mediator, MarketPlaceDbContext db, ICookieService cookieService)
        {

            _distributedCache = distributedCache;
            _mediator = mediator;
            _db = db;
            _cookieService = cookieService;

        }
        public async Task<BasketDTO> Add(string userId, int productId, int quantity)
        {

            if (userId is null)
            {


                var cookie = _cookieService.GetCookie("basket");
                if(cookie is not null)
                {
                    await _mediator.Send(new AddToBasketCookieCommand { ProductId = productId, Quantity = quantity });

                }



            }
            else
            {
                await _mediator.Send(new AddToBasketRedisCommand { UserId = userId, ProductId = productId, Quantity = quantity });

            }

            

            return await Get(userId);


        }

        public Task<bool> CheckBasketProductStock(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<BasketDTO> Get(string userId)
        {
            string data = "";

           
            if (userId is null)
            {
                var cookie = _cookieService.GetCookie("basket");
                if (cookie is not null)
                {
                    data = cookie;
                }
                else
                {
                    BasketDTO basketDTO = new BasketDTO();
                    data = Utf8Json.JsonSerializer.ToJsonString(basketDTO);
                    _cookieService.SetCookie("basket", data);
                }
            }
            else
            {
                data = _distributedCache.GetString("basket_" + userId);

                if (data is null)
                {
                    data = Utf8Json.JsonSerializer.ToJsonString(new BasketDTO
                    {
                        UserId = userId
                    });
                    _distributedCache.SetString("basket_" + userId, data);

                }             

            }

            var result = JsonSerializer.Deserialize<BasketDTO>(data);

            result.BasketProducts = result.BasketProducts.OrderBy(x => x.ProductId).ToList();

            return Task.FromResult(result);

        }

        public async Task<BasketDTO> Remove(string userId, int productId)
        {
            if (userId is null)
            {
                var cookie = _cookieService.GetCookie("basket");
                if (cookie is not null)
                {
                    await _mediator.Send(new RemoveItemBasketCookieCommand { ProductId = productId });

                    var items = Utf8Json.JsonSerializer.Deserialize<BasketDTO>(cookie);
                    var product = items.BasketProducts.FirstOrDefault(x => x.ProductId == productId  );

                    if (product is not null)
                    {
                        items.BasketProducts.Remove(product);
                        return items;
                    }

                }
            }
            else
            {
                var cache = _distributedCache.Get("basket_"+userId);
                if(cache is not null)
                {
                    await _mediator.Send(new RemoveItemBasketRedisCommand { ProductId = productId, UserId = userId });
                    var items = Utf8Json.JsonSerializer.Deserialize<BasketDTO>(cache);
                    var product = items.BasketProducts.FirstOrDefault(x => x.ProductId == productId);

                    if (product is not null)
                    {
                        items.BasketProducts.Remove(product);
                        return items;
                    }
                }
            }
           
            return await Get(userId);
        }

        public async Task<BasketDTO> RemoveAll(string userId)
        {
            BasketDTO basketDTO = new BasketDTO();

            if (userId is null)
            {
                _cookieService.SetCookie("basket", Utf8Json.JsonSerializer.ToJsonString(basketDTO));
            }
           

            else
            {
                _distributedCache.SetString("basket_"+userId, Utf8Json.JsonSerializer.ToJsonString(basketDTO));


            }
           

            return await Get(userId);

      

        }

        public Task<BasketDTO> Update(string userId, int productId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
