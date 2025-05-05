using Microsoft.EntityFrameworkCore;
using MrKatsuWebAPI.Application.Redis;
using MrKatsuWebAPI.DataAccess;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.Paging;
using MrKatsuWebAPI.DTO.PostRequest;
using MrKatsuWebAPI.Helper;

namespace MrKatsuWebAPI.Application.Posts
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _db;
        private readonly IRedisService _redis;
        private DateTime _now;
        public PostService(AppDbContext db, IRedisService redis)
        {
            _db = db;
            _redis = redis;
            _now = new TimeHelper.Builder()
                .SetTimestamp(DateTime.UtcNow)
                .SetTimeZone("SE Asia Standard Time")
                .SetRemoveTick(true).Build();
        }
        public async Task<ApiResult<int>> CreatePost(PostRequest request, int userId)
        {
            var today = _now.ToString("yyyy-MM-dd");
            var redisKey = $"user:{userId}:post_count:{today}";
            // Lấy số lượng post hiện tại từ Redis
            var redisValue = await _redis.GetValue<int?>(redisKey);
            var postsCreatedToday = redisValue.HasValue ? int.Parse(redisValue.ToString()) : 0;
            if (postsCreatedToday >= 3) return new ApiErrorResult<int>("Bạn đã đạt giới hạn tạo 3 bài post mỗi ngày");
            // Tạo post mới
            var post = new Post
            {
                Content = request.Content,
                Title = request.Title,
                UserId = userId,
                CreatedAt = _now,
                UpdatedAt = _now,
                IsDeleted = false,
            };
            _db.Posts.Add(post);
            await _db.SaveChangesAsync();
            await RemoveOldCache();
            // Tăng counter trong Redis và thiết lập time-to-live (tự động hết hạn sau 24 giờ)
            await _redis.IncrementValue(redisKey);
            // Nếu là lần post đầu tiên trong ngày, thiết lập TTL
            if (postsCreatedToday == 0)
            {
                // Tính thời gian đến nửa đêm hôm nay
                var midnight = DateTime.Today.AddDays(1);
                var secondsUntilMidnight = (int)(midnight - _now).TotalSeconds;
                await _redis.SetKeyExpire(redisKey, TimeSpan.FromSeconds(secondsUntilMidnight));
            }
            return new ApiSuccessResult<int>(post.Id);
        }

        public async Task<ApiResult<bool>> DeletePost(int id, int userId)
        {
            var post = await _db.Posts.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (post == null) return new ApiErrorResult<bool>("Không có quyền để xóa bài viết này");
            post.IsDeleted = true;
            _db.Posts.Update(post);
            await _db.SaveChangesAsync();
            await RemoveOldCache();
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<PostDetailViewModel>> GetPostById(int id, PagingRequest request)
        {
            var post = await _db.Posts.Include(x => x.User).FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
            if (post == null) return new ApiErrorResult<PostDetailViewModel>("Post not found or has been deleted");

            var postViewModel = new PostDetailViewModel
            {
                Id = post.Id,
                AuthorDisplayName = post.User.UserName!,
                AuthorAvatarUrl = post.User.Avatar,
                CreatedAt = post.CreatedAt,
                AuthorId = post.UserId,
                Content = post.Content,
                Title = post.Title,
            };
            return new ApiSuccessResult<PostDetailViewModel>(postViewModel);
        }

        public async Task<ApiResult<PagedResult<PostViewModel>>> GetPosts(PagingRequest request)
        {
            var postFacade = new GetPostFacade(_db, _redis);
            var result = await postFacade.GetPosts(request);
            return result;
        }

        public async Task<ApiResult<PagedResult<ReplyViewModel>>> GetRepliesByPostId(int postId, PagingRequest request)
        {
            var query = _db.Replies.Include(x => x.User).Where(x => x.PostId == postId && !x.IsDeleted).AsQueryable();
            var totalRow = await query.CountAsync();
            var replies = await query.OrderByDescending(x => x.CreatedAt)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ReplyViewModel
                {
                    Id = x.Id,
                    AuthorDisplayName = x.User.UserName!,
                    AuthorAvatarUrl = x.User.Avatar,
                    CreatedAt = x.CreatedAt,
                    AuthorId = x.UserId,
                    Content = x.Content,
                    ParentReplyId = x.ParentId
                }).ToListAsync();
            var pageResult = new PagedResult<ReplyViewModel>
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = replies
            };
            return new ApiSuccessResult<PagedResult<ReplyViewModel>>(pageResult);
        }

        public async Task<ApiResult<bool>> UpdatePost(int id, PostRequest request, int userId)
        {
            var post = await _db.Posts.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (post == null) return new ApiErrorResult<bool>("Post not found");
            post.Content = request.Content;
            post.Title = request.Title;
            post.UpdatedAt = _now;
            _db.Posts.Update(post);
            await _db.SaveChangesAsync();
            await RemoveOldCache();
            return new ApiSuccessResult<bool>(true);
        }
        private async Task RemoveOldCache()
        {
            await _redis.RemoveValue(SystemConstant.CACHE_POST);
        }
    }
}
