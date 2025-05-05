using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MrKatsuWebAPI.Application.Redis;
using MrKatsuWebAPI.DataAccess;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.Mods;
using MrKatsuWebAPI.DTO.Paging;
using MrKatsuWebAPI.Helper;

namespace MrKatsuWebAPI.Application.Mods
{
    public class ModService : IModService
    {
        private readonly AppDbContext _db;
        private readonly IRedisService _redis;
        private DateTime _now;
        private ILogger<ModService> _logger;
        public ModService(AppDbContext db, IRedisService redis, ILogger<ModService> logger)
        {
            _db = db;
            _logger = logger;
            _redis = redis;
            _now = new TimeHelper.Builder()
               .SetTimestamp(DateTime.UtcNow)
               .SetTimeZone("SE Asia Standard Time")
               .SetRemoveTick(true).Build();
        }
        public async Task<ApiResult<PagedResult<ModViewModel>>> GetMods(ModPagingRequest request)
        {
            var facade = new GetModFacade(_db, _redis);
            return await facade.GetMods(request);
        }
        public async Task<ApiResult<ModDetailViewModel>> GetModById(int id, PagingRequest request)
        {
            var mod = await _db.Mods.Include(x => x.User).FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
            if (mod == null) return new ApiErrorResult<ModDetailViewModel>("Mod not found or has been deleted");

            var urls = await _db.Urls.Where(x => x.ModId == id && !x.IsDeleted).Select(x => new UrlViewModel
            {
                Id = x.Id,
                Url = x.UrlString
            }).ToListAsync();
            var modViewModel = new ModDetailViewModel
            {
                Id = mod.Id,
                AuthorDisplayName = mod.User.UserName!,
                AuthorAvatarUrl = mod.User.Avatar,
                CreatedAt = mod.CreatedAt,
                UpdatedAt = mod.UpdatedAt,
                AuthorId = mod.UserId,
                IsPrivate = mod.IsPrivate,
                Content = mod.Description,
                Title = mod.Name,
                Urls = urls
            };
            return new ApiSuccessResult<ModDetailViewModel>(modViewModel);
        }
        public async Task<ApiResult<int>> CreateMod(ModCombineRequest request, int userId)
        {
            var newMod = new ModRequest()
            {
                Name = request.Name,
                Description = request.Description,
                IsPrivate = request.IsPrivate,
                CategoryId = request.CategoryId,
            };
            var modResult = await CreateInternalMod(newMod, userId);
            if (!modResult.Success) return modResult;
            var newUrl = new UrlRequest()
            {
                Url = request.NewUrls
            };
            var urlResult = await CreateInternalUrl(newUrl, modResult.Data);
            if (!urlResult.Success) return urlResult;
            _ = Task.Run(async () =>
            {
                await RemoveOldCache(SystemConstant.CACHE_MOD);
            });
            return new ApiSuccessResult<int>(modResult.Data);
        }
        private async Task<ApiResult<int>> CreateInternalMod(ModRequest request, int userId)
        {
            var mod = new Mod()
            {
                Name = request.Name,
                Description = request.Description,
                UserId = userId,
                CreatedAt = _now,
                UpdatedAt = _now,
                CategoryId = request.CategoryId,
                IsPrivate = request.IsPrivate,
                IsDeleted = false,
            };
            _db.Mods.Add(mod);
            await _db.SaveChangesAsync();
            return new ApiSuccessResult<int>(mod.Id);
        }
        private async Task<ApiResult<int>> CreateInternalUrl(UrlRequest request, int modId)
        {
            if (request.Url == null || !request.Url.Any())
            {
                return new ApiSuccessResult<int>(modId);
            }

            foreach (var u in request.Url)
            {
                var item = new Url()
                {
                    ModId = modId,
                    UrlString = u,
                    CreatedAt = _now,
                    IsDeleted = false,
                    UpdatedAt = _now
                };
                await _db.Urls.AddAsync(item).ConfigureAwait(false);
            }
            await _db.SaveChangesAsync();
            return new ApiSuccessResult<int>(modId);
        }
        public async Task<ApiResult<int>> UpdateMod(int id, ModUpdateCombineRequest request, int userId)
        {
            var update = new UpdateModFacade(_db, _now);
            var modResult = await update.UpdateMod(id, request, userId);
            _ = Task.Run(async () =>
            {
                await RemoveOldCache(SystemConstant.CACHE_MOD);
            });
            return new ApiSuccessResult<int>(modResult.Data);
        }
        public async Task<ApiResult<int>> DeleteUrls(int modId, UrlDeleteRequest request, int userId)
        {
            var urls = await _db.Urls.Where(x => x.ModId == modId && !x.IsDeleted && request.UrlId.Contains(x.Id)).ToListAsync();
            if (!urls.Any() || !request.UrlId.Any())
            {
                return new ApiSuccessResult<int>(modId);
            }
            foreach (var url in urls)
            {
                _db.Urls.Remove(url);
            }
            await _db.SaveChangesAsync();
            return new ApiSuccessResult<int>(modId);
        }
        public async Task<ApiResult<bool>> DeleteMod(int id, int useId)
        {
            var mod = await _db.Mods.FirstOrDefaultAsync(x => x.Id == id && x.UserId == useId);
            if (mod == null) return new ApiErrorResult<bool>("Mod not found or has been deleted");
            mod.IsDeleted = true;
            mod.UpdatedAt = _now;
            _db.Mods.Update(mod);
            await _db.SaveChangesAsync();
            _ = Task.Run(async () =>
            {
                await RemoveOldCache(SystemConstant.CACHE_MOD);
            });
            return new ApiSuccessResult<bool>(true);
        }
        public async Task<ApiResult<int>> CreateReaction(ReactionRequest request, int userId)
        {
            // Kiểm tra giới hạn spam trước khi cho phép tạo bình luận mới

            // 1. Kiểm tra số lượng bình luận trong khoảng thời gian ngắn (ví dụ: 5 bình luận/phút)
            var minuteRateKey = $"user:{userId}:reaction_rate:minute:{DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm")}";
            var recentCount = await _redis.GetValue<int?>(minuteRateKey);
            var recentCommentCount = recentCount.HasValue ? int.Parse(recentCount.ToString()) : 0;
            if (recentCommentCount >= 5) return new ApiErrorResult<int>("Bạn đang bình luận quá nhanh. Vui lòng đợi ít phút.");
            // 2. Kiểm tra số lượng bình luận trongngày (50 bình luận/ngày)
            var dailyRateKey = $"user:{userId}:reaction_rate:daily:{DateTime.UtcNow.ToString("yyyy-MM-dd")}";
            var dailyCount = await _redis.GetValue<int?>(dailyRateKey);
            var dailyCommentCount = recentCount.HasValue ? int.Parse(dailyCount.ToString()) : 0;
            if (dailyCommentCount >= 50) return new ApiErrorResult<int>("Bạn đã đạt giới hạn bình luận trong ngày. Vui lòng thử lại sau.");

            var mod = await _db.Mods.FirstOrDefaultAsync(x => x.Id == request.ModId && !x.IsDeleted);
            if (mod == null) return new ApiErrorResult<int>("Mod not found or has been deleted");
            var reaction = new Reaction()
            {
                ModId = request.ModId,
                UserId = userId,
                Content = request.Content,
                CreatedAt = _now,
                UpdatedAt = _now,
                IsDeleted = false
            };
            _db.Reactions.Add(reaction);
            await _db.SaveChangesAsync();

            await _redis.IncrementValue(minuteRateKey);
            await _redis.SetKeyExpire(minuteRateKey, TimeSpan.FromMinutes(3));

            await _redis.IncrementValue(dailyRateKey);
            if (dailyCommentCount == 0)
            {
                var midnight = DateTime.Today.AddDays(1);
                var secondsUntilMidnight = (int)(midnight - DateTime.Now).TotalSeconds;
                await _redis.SetKeyExpire(dailyRateKey, TimeSpan.FromSeconds(secondsUntilMidnight));
            }
            return new ApiSuccessResult<int>(reaction.Id);
        }
        public async Task<ApiResult<bool>> UpdateReaction(int id, ReactionRequest request, int userId)
        {
            var reaction = await _db.Reactions.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId && !x.IsDeleted);
            if (reaction == null) return new ApiErrorResult<bool>("Reaction not found or has been deleted");
            reaction.Content = request.Content;
            reaction.UpdatedAt = _now;
            _db.Reactions.Update(reaction);
            await _db.SaveChangesAsync();
            return new ApiSuccessResult<bool>(true);
        }
        public async Task<ApiResult<bool>> DeleteReaction(int id, int userId)
        {
            var reaction = await _db.Reactions.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId && !x.IsDeleted);
            if (reaction == null) return new ApiErrorResult<bool>("Reaction not found or has been deleted");
            _db.Reactions.Remove(reaction);
            await _db.SaveChangesAsync();
            return new ApiSuccessResult<bool>(true);
        }
        public async Task<ApiResult<ModInternalViewModel>> GetModInternalById(int id)
        {
            var mod = await _db.Mods.Include(x => x.User).FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
            if (mod == null) return new ApiErrorResult<ModInternalViewModel>("Mod not found or has been deleted");
            var urls = await _db.Urls.Where(x => x.ModId == id && !x.IsDeleted).Select(x => new UrlViewModel
            {
                Id = x.Id,
                Url = x.UrlString
            }).ToListAsync();
            var modVM = new ModInternalViewModel()
            {
                Id = mod.Id,
                Title = mod.Name,
                AuthorId = mod.UserId,
                CategoryId = mod.CategoryId,
                Description = mod.Description,
                IsPrivate = mod.IsPrivate,
                Urls = urls
            };
            return new ApiSuccessResult<ModInternalViewModel>(modVM);
        }
        public async Task<ApiResult<PagedResult<ReactViewModel>>> GetReactByModId(int modId, PagingRequest request)
        {
            var query = _db.Reactions.Include(x => x.User).Where(x => x.ModId == modId && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt).AsQueryable();
            var totalRecords = await query.CountAsync();
            var reactions = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ReactViewModel
                {
                    Id = x.Id,
                    AuthorDisplayName = x.User.UserName!,
                    AuthorAvatarUrl = x.User.Avatar,
                    CreatedAt = x.CreatedAt,
                    AuthorId = x.UserId,
                    Content = x.Content,
                    UpdateAt = x.UpdatedAt
                }).ToListAsync();
            var pagedResult = new PagedResult<ReactViewModel>()
            {
                Items = reactions,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
            };
            return new ApiSuccessResult<PagedResult<ReactViewModel>>(pagedResult);
        }
        private async Task RemoveOldCache(string cacheKey)
        {
            await _redis.RemoveValue(cacheKey);
        }
    }
}
