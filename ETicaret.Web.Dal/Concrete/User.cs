using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Dal.Concrete
{
    //[Table("ManagementUser",Schema ="Management")]
    public class User : BaseEntity

    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Mail { get; set; }

        public int PhoneNumber { get; set; }

        public int Password { get; set; }
   
        public List<Order> Orders { get; set; }

        public List<Comment> Comments { get; set; }











    }
}
