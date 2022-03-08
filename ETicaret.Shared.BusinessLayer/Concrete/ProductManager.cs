using ETicaret.Shared.BusinessLayer.Abstract;
using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public Product GetByID(int ID)
        {
            return _productDal.Find(x=>x.ID==ID);
        }

        public List<Product> GetList()
        {
            return _productDal.GetAll();
        }

        public void ProductAdd(Product product)
        {
            _productDal.Add(product);
        }

        public void UserDelete(Product product)
        {
            throw new NotImplementedException();
        }

        public void UserUpdate(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
