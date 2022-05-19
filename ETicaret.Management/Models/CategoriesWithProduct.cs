using ETicaret.Shared.Dal.Concrete;

namespace ETicaret.Management.Models
{
    public class CategoriesWithProduct
    {
        public List<Category> Categories { get; set; }
        public Product Product { get; set; }
    }
}
