using ETicaret.Shared.Application;
using ETicaret.Shared.Dal.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Caching.Memory;
using ETicaret.Shared.Repository.UnitOfWork;
using ETicaret.Shared.Dal;

namespace ETicaret.Management.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;
     public UserController(IMemoryCache memoryCache, IUnitOfWork unitOfWork, MarketPlaceDbContext context)
        {
            _memoryCache = memoryCache;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("[action]")]

        public ActionResult<IEnumerable<User>> Get()
        {
            const string key = "Kullanıcılar";

            if (_memoryCache.TryGetValue(key, out object list))
            return Ok(list);
            var userList = _unitOfWork.Users.GetAll();
;         
            _memoryCache.Set(key, userList, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(20),
                Priority = CacheItemPriority.Normal
            });
            return Ok(userList);
        }
    }
}
