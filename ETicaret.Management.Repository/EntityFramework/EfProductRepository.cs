
using ETicaret.Shared.Repository.Abstract;
using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Repository.EntityFramework
{
    public class EfProductRepository : Repository<Product>, IProductDal
    {

        private Context _context;
        public EfProductRepository(Context context) : base(context)
        {

            _context = context;
        }

        public List<Product> GetListWithCategory( )
        {
            return _context.Products.Include(p => p.Category).ToList();
        }
    }
}
