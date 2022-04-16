using ETicaret.Shared.Repository.Abstract;
using ETicaret.Shared.Repository.EntityFramework;
using ETicaret.Shared.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserDal Users { get; private set; }
        public IProductDal Products { get; private set; }
        public ICategoryDal Category { get; private set; }

        private readonly MarketPlaceDbContext _context;
        public UnitOfWork ()
        {
            _context = new MarketPlaceDbContext ();
            Users = new EfUserRepository(_context);
            Products = new EfProductRepository(_context);
            Category = new EfCategoryRepository(_context);

        }

       
        public void Dispose()
        {
             _context.Dispose();
        }

        public Task<int> SaveAsync()
        {
           return _context.SaveChangesAsync();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
