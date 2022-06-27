using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Web.Application.DTOs
{
    public class BasketDTO 
    {
        public string UserId { get; set; }

        public List<BasketProducts> BasketProducts { get; set; } = new List<BasketProducts> ();

        public decimal SubTotal => Math.Round(BasketProducts.Sum(s => s.SubTotal), 2, MidpointRounding.AwayFromZero);

        public int ShippingAddressId { get; set; }
        public int BillingAddressId { get; set; } = 0;
    }
    public class BasketProducts
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public string Path { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal => ProductPrice * Quantity;
  
         

    }
  



}
