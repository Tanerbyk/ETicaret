using ETicaret.Shared.Dal.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.BusinessLayer.Abstract
{
    public interface IProductService
    {
        void ProductAdd(Product product);

        void UserDelete(Product product);
        void UserUpdate(Product product);

        List<Product> GetList();

        Product GetByID(int ID);
    }
}
