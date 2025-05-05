using Microsoft.EntityFrameworkCore;
using MrKatsuWebAPI.DataAccess;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.Mods;
using MrKatsuWebAPI.Helper;
using System.Linq;

namespace MrKatsuWebAPI.Application.Mods
{
    public class UpdateModFacade(AppDbContext _db, DateTime _now)
    {
        public async Task<ApiResult<int>> UpdateMod(int id, ModUpdateCombineRequest request, int userId)
        {
            // 1. Cập nhật thông tin Mod chính
            var updateMod = new ModRequest()
            {
                Name = request.Name,
                Description = request.Description,
                IsPrivate = request.IsPrivate,
                CategoryId = request.CategoryId,
            };

            var modResult = await UpdateInternalMod(updateMod, id, userId);
            if (!modResult.Success) return modResult;

            // 2. Xử lý URLs - Thứ tự quan trọng: Xóa -> Cập nhật -> Thêm mới
            if (request.UrlIdsToDelete != null && request.UrlIdsToDelete.Any())
            {
                var deleteResult = await DeleteUrls(id, request.UrlIdsToDelete, userId);
                if (!deleteResult.Success) return deleteResult;
            }

            if (request.UpdatedUrls != null && request.UpdatedUrls.Any())
            {
                var updateResult = await UpdateUrls(id, request.UpdatedUrls, userId);
                if (!updateResult.Success) return updateResult;
            }

            if (request.NewUrls != null && request.NewUrls.Any())
            {
                var addResult = await AddNewUrls(id, request.NewUrls);
                if (!addResult.Success) return addResult;
            }

            return new ApiSuccessResult<int>(id);
        }
        private async Task<ApiResult<int>> UpdateInternalMod(ModRequest request, int id, int userId)
        {
            var mod = await _db.Mods.Include(x => x.User)
                          .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (mod == null)
                return new ApiErrorResult<int>("Mod not found or has been deleted");

            mod.Name = request.Name;
            mod.Description = request.Description;
            mod.IsPrivate = request.IsPrivate;
            mod.UpdatedAt = _now;

            _db.Mods.Update(mod);
            await _db.SaveChangesAsync();

            return new ApiSuccessResult<int>(mod.Id);
        }
        private async Task<ApiResult<int>> AddNewUrls(int modId, List<string> newUrls)
        {
            foreach (var url in newUrls)
            {
                var newUrl = new Url()
                {
                    ModId = modId,
                    UrlString = url,
                    CreatedAt = _now,
                    UpdatedAt = _now,
                    IsDeleted = false
                };
                await _db.Urls.AddAsync(newUrl);
            }

            await _db.SaveChangesAsync();
            return new ApiSuccessResult<int>(modId);
        }
        private async Task<ApiResult<int>> UpdateUrls(int modId, List<UrlUpdateRequest> updatedUrls, int userId)
        {
            // Lấy tất cả URLs cần cập nhật trong một query
            var urlIds = updatedUrls.Select(x => x.Id).ToList();
            var existingUrls = await _db.Urls
                .Where(x => x.ModId == modId && urlIds.Contains(x.Id))
                .ToListAsync();

            foreach (var url in existingUrls)
            {
                var updateRequest = updatedUrls.FirstOrDefault(x => x.Id == url.Id);
                if (updateRequest != null)
                {
                    url.UrlString = updateRequest.Url;
                    url.UpdatedAt = _now;
                }
            }

            await _db.SaveChangesAsync();
            return new ApiSuccessResult<int>(modId);
        }
        private async Task<ApiResult<int>> DeleteUrls(int modId, List<int> urlIds, int userId)
        {
            // Chỉ xóa URLs thuộc về mod này
            var urlsToDelete = await _db.Urls
                .Where(x => x.ModId == modId && urlIds.Contains(x.Id))
                .ToListAsync();

            if (urlsToDelete.Any())
            {
                _db.Urls.RemoveRange(urlsToDelete);
                await _db.SaveChangesAsync();
            }

            return new ApiSuccessResult<int>(modId);
        }
    }
}
