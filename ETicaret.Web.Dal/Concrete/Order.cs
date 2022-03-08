using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Dal.Concrete
{
    public class Order : BaseEntity
    {
        [Key]
        public int OrderID { get; set; }
        public User User { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public int Total { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
        




    }
}
