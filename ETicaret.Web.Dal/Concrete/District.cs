using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Dal.Concrete
{
    public class District 
    {
        [Key]
        public int DistrictId { get; set; }
        public int CityId { get; set; }

        public City City { get; set; }

        public string CityName { get; set; }

        



    }
}
