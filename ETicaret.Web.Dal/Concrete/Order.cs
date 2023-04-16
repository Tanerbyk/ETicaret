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
            public int Id { get; set; }
            public string UserId { get; set; }
    
            public DateTime OrderDate { get; set; } = DateTime.UtcNow;
            public decimal TotalPrice { get; set; }

            public List<OrderDetail> OrderDetails { get; set; }





        }
}
