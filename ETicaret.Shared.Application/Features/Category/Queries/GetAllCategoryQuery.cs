using ETicaret.Shared.Application.Enums;
using ETicaret.Shared.Application.Extensions;
using ETicaret.Shared.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace ETicaret.Shared.Application.Features.Category.Queries
{
    //List<Category>

    public class GetAllCategoryQuery : IRequest<List<Dal.Concrete.Category>>
    {
 
        public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<Dal.Concrete.Category>>
        {
            private readonly MarketPlaceDbContext _db;
            private readonly IDistributedCache _distributedCache;
            public GetAllCategoryQueryHandler(MarketPlaceDbContext db, IDistributedCache distributedCache)
            {
                _db = db;
                _distributedCache = distributedCache;
            }
            public async Task<List<Dal.Concrete.Category>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
            {
                string key = CacheKeys.CategoryList.GetDisplayName();

                string cache = _distributedCache.GetString(key);


                if (cache is not null)
                {
                    var values =  Utf8Json.JsonSerializer.Deserialize<List<Dal.Concrete.Category>>(cache.ToString());
                    return values;
                }
                else
                {
                    var values = await _db.Categories.ToListAsync();
                    _distributedCache.SetString(key, Utf8Json.JsonSerializer.ToJsonString(values));
                    return values;
                }
                

            }
        }

    }
}
