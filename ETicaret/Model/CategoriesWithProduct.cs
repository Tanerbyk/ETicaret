using ETicaret.Shared.Dal.Concrete;
using System.Collections.Generic;

namespace ETicaret.Web.Model
{
    public class CategoriesWithProduct
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}
