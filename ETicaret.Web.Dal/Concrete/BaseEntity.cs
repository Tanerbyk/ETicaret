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
      
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public DateTime ModifiedDate { get; set; } =DateTime.UtcNow;

        public bool IsActive { get; set; } = true;


    }
}
