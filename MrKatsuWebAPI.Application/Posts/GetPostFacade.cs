using Microsoft.EntityFrameworkCore;
using MrKatsuWebAPI.Application.Redis;
using MrKatsuWebAPI.DataAccess;
using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.Paging;
using MrKatsuWebAPI.DTO.PostRequest;
using MrKatsuWebAPI.Helper;

namespace MrKatsuWebAPI.Application.Posts
{
    public class GetPostFacade
    {
        private readonly AppDbContext _db;
        private readonly IRedisService _redis;
        public GetPostFacade(AppDbContext db, IRedisService redis)
        {
            _db = db;
            _redis = redis;
        }
        public async Task<ApiResult<PagedResult<PostViewModel>>> GetPosts(PagingRequest request)
        {
            const string cacheKey = SystemConstant.CACHE_POST;
            List<PostViewModel>? cachedData = null;
            bool useCache = await _redis.KeyExist(cacheKey);

            if (useCache)
            {
                cachedData = await _redis.GetValue<List<PostViewModel>>(cacheKey);
            }

            PagedResult<PostViewModel> result;

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

            return new ApiSuccessResult<PagedResult<PostViewModel>>(result);
        }
        private IQueryable<PostViewModel> BuildBaseQuery()
        {
            return _db.Posts.Include(x => x.User)
                .Where(x => !x.IsDeleted)
                .Select(x => new PostViewModel
                {
                    Id = x.Id,
                    AuthorDisplayName = x.User.UserName!,
                    AuthorAvatarUrl = x.User.Avatar,
                    CreatedAt = x.CreatedAt,
                    AuthorId = x.UserId,
                    CommentCount = _db.Replies.Count(r => r.PostId == x.Id),
                    Title = x.Title,
                    UpdatedAt = x.UpdatedAt
                });
        }
        private List<PostViewModel> ApplyMemoryFilters(List<PostViewModel> data, PagingRequest request)
        {
            return data.OrderByDescending(x => x.UpdatedAt).ToList();
        }
        private IQueryable<PostViewModel> ApplyDatabaseFilters(IQueryable<PostViewModel> query, PagingRequest request)
        {
            return query.OrderByDescending(x => x.UpdatedAt);
        }
        private async Task<PagedResult<PostViewModel>> ExecuteDatabasePaging(IQueryable<PostViewModel> query, PagingRequest request)
        {
            var total = await query.CountAsync();
            var items = await query
                 .Skip((request.PageIndex - 1) * request.PageSize)
                 .Take(request.PageSize)
                 .ToListAsync();

            return new PagedResult<PostViewModel>
            {
                TotalRecords = total,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = items
            };
        }
        private PagedResult<PostViewModel> CreatePagedResult(List<PostViewModel> data, PagingRequest request)
        {
            return new PagedResult<PostViewModel>
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
        private async Task CacheBaseData(IQueryable<PostViewModel> query, string cacheKey)
        {
            var cacheData = await query
                .OrderByDescending(x => x.UpdatedAt)
                .ToListAsync();

            await _redis.SetValue(cacheKey, cacheData);
        }
    }
}
