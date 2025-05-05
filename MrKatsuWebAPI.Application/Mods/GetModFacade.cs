using Microsoft.EntityFrameworkCore;
using MrKatsuWebAPI.Application.Redis;
using MrKatsuWebAPI.DataAccess;
using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.Mods;
using MrKatsuWebAPI.DTO.Paging;
using MrKatsuWebAPI.DTO.PostRequest;
using MrKatsuWebAPI.Helper;

namespace MrKatsuWebAPI.Application.Mods
{
    public class GetModFacade
    {
        private readonly AppDbContext _db;
        private readonly IRedisService _redis;
        public GetModFacade(AppDbContext db, IRedisService redis)
        {
            _db = db;
            _redis = redis;
        }
        public async Task<ApiResult<PagedResult<ModViewModel>>> GetMods(ModPagingRequest request)
        {
            const string cacheKey = SystemConstant.CACHE_MOD;
            List<ModViewModel>? cachedData = null;
            bool useCache = await _redis.KeyExist(cacheKey);

            if (useCache)
            {
                cachedData = await _redis.GetValue<List<ModViewModel>>(cacheKey);
            }

            PagedResult<ModViewModel> result;

            if (useCache && cachedData != null && cachedData.Count > 0)
            {
                var filteredData = ApplyMemoryFilters(cachedData, request);
                result = CreatePagedResult(filteredData, request);
            }
            else
            {
                var query = BuildBaseQuery();
                var filteredQuery = ApplyDatabaseFilters(query, request);
                result = await ExecuteDatabasePaging(filteredQuery, request);

                await CacheBaseData(query, cacheKey);
            }

            return new ApiSuccessResult<PagedResult<ModViewModel>>(result);
        }
        private IQueryable<ModViewModel> BuildBaseQuery()
        {
            return _db.Mods
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt).Select(x => new ModViewModel()
                {
                    Id = x.Id,
                    Title = x.Name,
                    AuthorId = x.UserId,
                    AuthorDisplayName = x.User.UserName!,
                    AuthorAvatarUrl = x.User.Avatar,
                    Category = x.Category.Name,
                    IsPrivate = x.IsPrivate,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                });
        }
        private List<ModViewModel> ApplyMemoryFilters(List<ModViewModel> data, ModPagingRequest request)
        {
            var query = data.AsQueryable();
            if (!string.IsNullOrEmpty(request.KeyWord) && request.KeyWord == "private")
                query = query.Where(x => x.AuthorDisplayName == request.Username);
            if (!string.IsNullOrEmpty(request.Username))
                query = query.Where(x => x.AuthorDisplayName == request.Username);
            if (!string.IsNullOrEmpty(request.Category))
                query = query.Where(x => x.Category == request.Category);
            return query
               .OrderByDescending(x => x.UpdatedAt)
               .ToList();
        }
        private IQueryable<ModViewModel> ApplyDatabaseFilters(IQueryable<ModViewModel> query, ModPagingRequest request)
        {
            if (!string.IsNullOrEmpty(request.Username))
                query = query.Where(x => x.AuthorDisplayName == request.Username);
            if (!string.IsNullOrEmpty(request.Category))
                query = query.Where(x => x.Category == request.Category);
            if (!string.IsNullOrEmpty(request.KeyWord) && request.KeyWord == "private")
                query = query.Where(x => x.IsPrivate);

            return query.OrderByDescending(x => x.UpdatedAt);
        }
        private async Task<PagedResult<ModViewModel>> ExecuteDatabasePaging(IQueryable<ModViewModel> query, ModPagingRequest request)
        {
            var total = await query.CountAsync();
            var items = await query
                 .Skip((request.PageIndex - 1) * request.PageSize)
                 .Take(request.PageSize)
                 .ToListAsync();

            return new PagedResult<ModViewModel>
            {
                TotalRecords = total,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = items
            };
        }
        private PagedResult<ModViewModel> CreatePagedResult(List<ModViewModel> data, ModPagingRequest request)
        {
            return new PagedResult<ModViewModel>
            {
                TotalRecords = data.Count,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
                    .Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList()
            };
        }
        private async Task CacheBaseData(IQueryable<ModViewModel> query, string cacheKey)
        {
            var cacheData = await query
                .OrderByDescending(x => x.UpdatedAt)
                .ToListAsync();

            await _redis.SetValue(cacheKey, cacheData);
        }
    }
}
