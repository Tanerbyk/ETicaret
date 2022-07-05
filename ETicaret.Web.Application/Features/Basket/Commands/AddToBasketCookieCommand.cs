using ETicaret.Shared.Dal;
using ETicaret.Web.Application.Cookie;
using ETicaret.Web.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utf8Json;

namespace ETicaret.Web.Application.Features.Basket.Commands
{
    public class AddToBasketCookieCommand : IRequest<string>
    { 
        public int ProductId { get; set; }
        public int Quantity { get; set; }
       
        public class AddToBasketCookieCommandHandler : IRequestHandler<AddToBasketCookieCommand, string>
        {
            private readonly ICookieService _cookieService;
            private readonly MarketPlaceDbContext _db;

            public AddToBasketCookieCommandHandler(ICookieService cookieService, MarketPlaceDbContext db)
            {
                _cookieService = cookieService;
                _db = db;
            }

            public async Task<string> Handle(AddToBasketCookieCommand request, CancellationToken cancellationToken)
            {
                var cookie = _cookieService.GetCookie("basket");
                BasketDTO cookieModel = new BasketDTO();
                if (cookie is not null)
                {
                    cookieModel = JsonSerializer.Deserialize<BasketDTO>(cookie);
                }

                var product =  _db.Products

                    .FirstOrDefault(x => x.Id == request.ProductId);

            
                if (!cookieModel.BasketProducts.Any())
                {
                    BasketDTO basketDTO = new BasketDTO
                    {
                        UserId = string.Empty,
                        BasketProducts = new List<BasketProducts>
                        {
                             new BasketProducts  {
                            ProductId = request.ProductId,
                            Quantity = request.Quantity,
                                Path = product.Path,

                            ProductPrice = product.Price,

                             }
                        }
                    };
                    _cookieService.SetCookie("basket", JsonSerializer.ToJsonString(basketDTO));
                    cookieModel = basketDTO;
                }
                else
                {

                    var cookieProduct = cookieModel.BasketProducts.FirstOrDefault(x =>
                        x.ProductId == request.ProductId ); //gelen ürün sepette var mı
                    if (cookieProduct is null)
                    {
                        BasketProducts currentProduct = new BasketProducts
                        {
                            ProductId = request.ProductId,
                            Quantity = request.Quantity,
                            Path = product.Path,

                            ProductPrice = product.Price,



                        };
                        cookieModel.BasketProducts.Add(currentProduct);
                    }
                    else
                    {
                        int currentQuantity = cookieProduct.Quantity + request.Quantity;
                      
                        cookieProduct.Quantity = currentQuantity;

                    }

                    _cookieService.SetCookie("basket", JsonSerializer.ToJsonString(cookieModel));
                }

                return "success";
            }
        }
    }
}
