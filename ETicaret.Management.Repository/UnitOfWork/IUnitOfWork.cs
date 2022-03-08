using ETicaret.Shared.Repository.Abstract;
using ETicaret.Shared.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserDal Users { get; }
        IProductDal Products { get; }      
        ICategoryDal Category { get; }

        int Save();

    }
}
