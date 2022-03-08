using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Dal.Concrete
{
    public class Category : BaseEntity
    {
        [Key]
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public int? parent_Id { get; set; }

        public List<Product> Products { get; set; }


    }
}
