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
       
        public int Id { get; set; }
        public string Name { get; set; }

        public int? parent_Id { get; set; }

        public List<Product> Products { get; set; }


    }
}
