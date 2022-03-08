using ETicaret.Shared.BusinessLayer.Abstract;
using ETicaret.Shared.Repository.Abstract;
using ETicaret.Shared.Dal.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal _userDal)
        {
            this._userDal = _userDal;
        }

        public User GetByID( int ID)
        {
            return _userDal.Find(x => x.ID == ID);

        }
        public List<User> GetList()
        {
            throw new NotImplementedException();
        }

        public void UserAdd(User user)
        {
            throw new NotImplementedException();
        }

        public void UserDelete(User user)
        {
            throw new NotImplementedException();
        }

        public void UserUpdate(User user)
        {
            throw new NotImplementedException();
        }
    }
}
