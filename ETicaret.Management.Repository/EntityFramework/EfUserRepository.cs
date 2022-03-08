
using ETicaret.Shared.Repository.Abstract;
using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Repository.EntityFramework
{
    public class EfUserRepository : Repository<User>, IUserDal
    {
        private Context _context;

        public EfUserRepository(Context context) : base(context)
        {
            _context = context; 
        }

        //public List<User> GetActiveUsers()
        //{
        //   return _context.Users.Where(x=>x.IsActive).ToList();
        //}

      
    }
}
