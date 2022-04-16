
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

        private MarketPlaceDbContext _context;
        public EfProductRepository(MarketPlaceDbContext context) : base(context)
        {

            _context = context;
        }

        public async Task<List<Product>> GetListWithCategory( )
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }
    }
}
