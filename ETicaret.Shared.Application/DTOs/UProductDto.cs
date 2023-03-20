using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.DTOs
{
    public class UProductDto
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }

        public string Path { get; set; }

        public IFormFile fileImage { get; set; }

        public int CategoryID { get; set; }

        public int Price { get; set; }
        public int Discount { get; set; }
    }
}
