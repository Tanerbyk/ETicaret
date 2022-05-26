using ETicaret.Shared.Dal.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Path { get; set; }

        public int Stock { get; set; }

        public string Description { get; set; }

        public int Discount { get; set; }

        public Category Category { get; set; }
        public decimal TaxPrice => Convert.ToDecimal(Price * 1.18);

        public decimal TotalPrice => Convert.ToDecimal(Price * 1.18 - (Price));






    }
}
