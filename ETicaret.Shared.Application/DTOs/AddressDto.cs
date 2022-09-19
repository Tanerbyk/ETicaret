using ETicaret.Shared.Dal.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.DTOs
{
    public class AddressDto
    {
        public int CityId { get; set; }

        public int DistrictId { get; set; }

        public string FullAddress { get; set; }

        public List<City> Cities { get; set; }
        public List<District> Districts { get; set; }
    }
}
