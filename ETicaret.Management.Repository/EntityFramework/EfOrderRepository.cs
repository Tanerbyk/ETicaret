
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
    public class EfOrderRepository : Repository<Order>, IOrderDal
    {


        public EfOrderRepository(MarketPlaceDbContext context) : base(context)
        {
        }
    }
}
