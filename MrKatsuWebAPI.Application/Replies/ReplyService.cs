using Microsoft.EntityFrameworkCore;
using MrKatsuWebAPI.Application.Redis;
using MrKatsuWebAPI.DataAccess;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.Paging;
using MrKatsuWebAPI.DTO.PostRequest;
using MrKatsuWebAPI.DTO.ReplyRequest;
using MrKatsuWebAPI.Helper;
using StackExchange.Redis;

namespace MrKatsuWebAPI.Application.Replies
{
    public class ReplyService : IReplyService
    {
        private readonly AppDbContext _db;
        private readonly IRedisService _redis;
        private DateTime _now;
        public ReplyService(AppDbContext db, IRedisService redis)
        {
            _db = db;
            _now = new TimeHelper.Builder()
               .SetTimestamp(DateTime.UtcNow)
               .SetTimeZone("SE Asia Standard Time")
               .SetRemoveTick(true).Build();
            _redis = redis;
        }
        public async Task<ApiResult<int>> CreateReply(ReplyRequest request, int userId)
        {
            // 1. Kiểm tra số lượng bình luận trong khoảng thời gian ngắn (ví dụ: 5 bình luận/phút)
            var minuteRateKey = $"user:{userId}:reply_rate:minute:{DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm")}";
            var recentCount = await _redis.GetValue<int?>(minuteRateKey);
            var recentCommentCount = recentCount.HasValue ? int.Parse(recentCount.ToString()) : 0;
            if (recentCommentCount >= 5) return new ApiErrorResult<int>("Bạn đang bình luận quá nhanh. Vui lòng đợi ít phút.");
            // 2. Kiểm tra số lượng bình luận trongngày (50 bình luận/ngày)
            var dailyRateKey = $"user:{userId}:reply_rate:daily:{DateTime.UtcNow.ToString("yyyy-MM-dd")}";
            var dailyCount = await _redis.GetValue<int?>(dailyRateKey);
            var dailyCommentCount = recentCount.HasValue ? int.Parse(dailyCount.ToString()) : 0;
            if (dailyCommentCount >= 50) return new ApiErrorResult<int>("Bạn đã đạt giới hạn bình luận trong ngày. Vui lòng thử lại sau.");

            var post = await _db.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId && !x.IsDeleted && !x.IsLocked);
            if (post == null) return new ApiErrorResult<int>("Post not found or has been deleted");
            var newReply = new Reply
            {
                Content = request.Content,
                PostId = request.PostId,
                UserId = userId,
                CreatedAt = _now,
                UpdatedAt = _now,
                IsDeleted = false,                
            };
            _db.Replies.Add(newReply);

            post.UpdatedAt = _now;
            _db.Posts.Update(post);
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
            return new ApiSuccessResult<int>(newReply.Id);
        }

        public async Task<ApiResult<bool>> DeleteReply(int id, int userId)
        {
            var reply = _db.Replies.Include(x => x.User).FirstOrDefault(x => x.Id == id && !x.IsDeleted && x.UserId == userId);
            if (reply == null) return new ApiErrorResult<bool>("Reply not found or has been deleted");
            reply.IsDeleted = true;
            _db.Replies.Remove(reply);
            await _db.SaveChangesAsync();
            return new ApiSuccessResult<bool>(true);
        }

        public Task<ApiResult<PagedResult<ReplyViewModel>>> GetReplies(int postId, PagingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<ReplyViewModel>> GetReplyById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> UpdateReply(int id, ReplyRequest request, int userId)
        {
            var reply = _db.Replies.FirstOrDefault(x => x.Id == id && !x.IsDeleted && x.UserId == userId);
            if (reply == null) return new ApiErrorResult<bool>("Reply not found or has been deleted");
            reply.Content = request.Content;
            reply.UpdatedAt = _now;
            _db.Replies.Update(reply);
            await _db.SaveChangesAsync();
            return new ApiSuccessResult<bool>(true);

        }
    }
}
