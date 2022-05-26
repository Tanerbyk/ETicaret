using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Dal.Concrete
{
    public class Product : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }     

        public string Path { get; set; }

        public int Stock { get; set; }

        public int Discount { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public List<Comment> Comments { get; set; }

        public List<User> Users { get; set; }







    }
}
