using ETicaret.Shared.Dal.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Repository.Abstract
{
    public interface IUserDal : IRepository<User>
    {
        //List<User> GetActiveUsers();
    }
}
