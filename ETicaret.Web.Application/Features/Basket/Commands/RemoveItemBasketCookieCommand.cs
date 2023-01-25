using ETicaret.Shared.Dal;
using ETicaret.Web.Application.Cookie;
using ETicaret.Web.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Web.Application.Features.Basket.Commands
{
    public class RemoveItemBasketCookieCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        private readonly ICookieService _cookieService;
        private readonly MarketPlaceDbContext _db;




        public class RemoveItemBasketCookieCommandHandler : IRequestHandler<RemoveItemBasketCookieCommand, bool>
        {
            private readonly ICookieService _cookieService;
            private readonly MarketPlaceDbContext _db;

            public RemoveItemBasketCookieCommandHandler(ICookieService cookieService, MarketPlaceDbContext db)
            {
                _cookieService = cookieService;
                _db = db;
            }

            public async Task<bool> Handle(RemoveItemBasketCookieCommand request, CancellationToken cancellationToken)
            {
                var cookie = _cookieService.GetCookie("basket");
                BasketDTO cookieModel = new BasketDTO();

                if (cookie is not  null)
                {
                    cookieModel = Utf8Json.JsonSerializer.Deserialize<BasketDTO>(cookie);
                    var deletedItem = cookieModel.BasketProducts.FirstOrDefault(x => x.ProductId == request.ProductId );
                    cookieModel.BasketProducts.Remove(deletedItem);
                    _cookieService.SetCookie("basket", Utf8Json.JsonSerializer.ToJsonString(cookieModel));
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

