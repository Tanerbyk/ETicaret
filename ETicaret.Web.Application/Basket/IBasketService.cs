using ETicaret.Web.Application.DTOs;

namespace ETicaret.Web.Application.Basket
{
    public interface IBasketService
    {
        Task<BasketDTO> Get(string  userId);

        Task<BasketDTO> Add(string userId,int productId,int quantity);
        Task<BasketDTO> Update(string userId,int productId,int quantity);
        Task<BasketDTO> Remove(string userId,int productId);
        Task<BasketDTO> RemoveAll(string userId);

        Task<bool> CheckBasketProductStock(string userId);



    }
}
