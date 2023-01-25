using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Dal.Concrete
{
    public class Address : BaseEntity
    {

        public int AddressId { get; set; }


        public string AddressTitle { get; set; }
        public string FullAddress { get; set; }
        public City city { get; set; }
        public int CityId { get; set; }
        public District district { get; set; }
        public int DistrictId { get; set; }

   

        
        public string UserId  { get; set; }

        public string AddressDetail { get; set; }







    }
}
