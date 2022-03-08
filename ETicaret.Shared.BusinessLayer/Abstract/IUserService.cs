using ETicaret.Shared.Dal.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.BusinessLayer.Abstract
{
    public interface IUserService
    {
        void UserAdd(User user);

        void UserDelete(User user);
        void UserUpdate(User user);

        List<User> GetList();

        User GetByID(int  ID);
    }
}
