using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Dal.Concrete
{
    public class BaseEntity
    {
      
        public DateTime CreateDate { get; set; } = DateTime.Parse(DateTime.Now.ToShortTimeString());

        public DateTime ModifiedDate { get; set; } 

        public bool IsActive { get; set; } = true;


    }
}
