using ETicaret.Shared.Application.Features.Product.Commands;
using ETicaret.Shared.Dal.Concrete;

namespace ETicaret.Management.Models
{
 
    

    public class CategoriesWithProducts
    {
        public List<Category> Categories { get; set; }

        public Product Product { get; set; }
    }
}
