
using ETicaret.Shared.Repository.Abstract;
using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Repository.EntityFramework
{
    public class EfCategoryRepository : Repository<Category>, ICategoryDal
    {
        Context _context;
        public EfCategoryRepository(Context context) : base(context)
        {
            _context = context;

        }
    }
}
