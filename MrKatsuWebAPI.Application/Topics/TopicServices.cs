using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MrKatsuWebAPI.Application.Comments;
using MrKatsuWebAPI.DataAccess;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.CommentRequest;
using MrKatsuWebAPI.DTO.TopicRequest;

namespace MrKatsuWebAPI.Application.Topics
{
    public class TopicServices : ITopicService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly MongoDbContext _context;
        public TopicServices(ICommentRepository commentRepository,
            MongoDbContext context,
            ITopicRepository topicRepository)
        {
            _commentRepository = commentRepository;
            _topicRepository = topicRepository;
            _context = context;
        }
        public async Task<ApiResult<string>> Comment(CommentCreateRequest request, string userId)
        {
            var topic = await _topicRepository.GetByIdAsync(request.TopicId);
            if (topic == null)
                return new ApiErrorResult<string>("Invalid TopicId");

            if (!string.IsNullOrEmpty(request.ParentCommentId))
            {
                var parent = await _commentRepository.GetByIdAsync(request.ParentCommentId);
                if (parent == null || parent.TopicId != request.TopicId)
                    return new ApiErrorResult<string>("Invalid ParentCommentId");
            }

            var newComment = new Comment
            {
                TopicId = request.TopicId,
                Content = request.Content,
                ParentCommentId = request.ParentCommentId,
                AuthorId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Dislikes = new List<string>(),
                Likes = new List<string>(),
                IsDeleted = false
            };
            await _commentRepository.CreateAsync(newComment);
            return new ApiSuccessResult<string>(newComment.Id);
        }
        private static readonly Dictionary<string, (string DisplayName, string AvatarUrl)> _userCache = new();
        public async Task<ApiResult<List<TopicViewModel>>> GetTopics(int skip = 0, int limit = 10)
        {
            // Bước 1: Lấy danh sách topic bằng LINQ
            var topics = await _context.Topics
                .Find(_ => true)
                .Skip(skip)
                .Limit(limit)
                .Project(t => new
                {
                    t.Id,
                    t.Title,
                    t.Content,
                    t.AuthorId,
                    t.CreatedAt,
                    t.UpdatedAt,
                    t.CommentCount
                })
                .ToListAsync();

            // Bước 2: Lấy danh sách AuthorId duy nhất
            var authorIds = topics.Select(t => t.AuthorId).Distinct().ToList();
            var usersToFetch = authorIds.Where(id => !_userCache.ContainsKey(id)).ToList();
            // Bước 3: Lấy thông tin người dùng từ Users
            if (usersToFetch.Any())
            {
                var users = await _context.Users
                    .Find(u => usersToFetch.Contains(u.Id))
                    .Project(u => new
                    {
                        u.Id,
                        u.Profile.DisplayName,
                        u.Profile.AvatarUrl
                    })
                    .ToListAsync();

                foreach (var user in users)
                {
                    _userCache[user.Id] = (user.DisplayName, user.AvatarUrl);
                }
            }

            // Bước 4: Kết hợp dữ liệu thành TopicViewModel
            var result = topics.OrderByDescending(x => x.UpdatedAt)
                .Select(t => new TopicViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Content = t.Content,
                    AuthorId = t.AuthorId,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    CommentCount = t.CommentCount,
                    AuthorDisplayName = _userCache.TryGetValue(t.AuthorId, out var user) ? user.DisplayName : "Unknown",
                    AuthorAvatarUrl = _userCache.TryGetValue(t.AuthorId, out user) ? user.AvatarUrl : ""
                }).ToList();

            return new ApiSuccessResult<List<TopicViewModel>>(result);
        }

        public async Task<ApiResult<TopicDetailViewModel>> GetTopicDetail(string topicId, int skip = 0, int limit = 10)
        {
            var pipeline = new List<BsonDocument>
            {
                // Lọc topic theo topicId
                new BsonDocument("$match",
                    new BsonDocument("_id", new ObjectId(topicId))),
                // Kết hợp với Users để lấy thông tin tác giả topic
                new BsonDocument("$lookup",
                    new BsonDocument
                    {
                        { "from", "Users" },
                        { "localField", "AuthorId" },
                        { "foreignField", "_id" },
                        { "as", "author" }
                    }),
                new BsonDocument("$unwind",
                    new BsonDocument
                    {
                        { "path", "$author" },
                        { "preserveNullAndEmptyArrays", true }
                    }),
                // Kết hợp với Comments để lấy danh sách bình luận
                new BsonDocument("$lookup",
                    new BsonDocument
                    {
                        { "from", "Comments" },
                        { "let", new BsonDocument("topicId", "$_id") },
                        { "pipeline", new BsonArray
                            {
                                new BsonDocument("$match",
                                    new BsonDocument
                                    {
                                        { "$expr", new BsonDocument("$and", new BsonArray
                                            {
                                                new BsonDocument("$eq", new BsonArray { "$TopicId", "$$topicId" }),
                                                new BsonDocument("$eq", new BsonArray { "$IsDeleted", false })
                                            })
                                        }
                                    }),
                                new BsonDocument("$lookup",
                                    new BsonDocument
                                    {
                                        { "from", "Users" },
                                        { "localField", "AuthorId" },
                                        { "foreignField", "_id" },
                                        { "as", "commentAuthor" }
                                    }),
                                new BsonDocument("$unwind",
                                    new BsonDocument
                                    {
                                        { "path", "$commentAuthor" },
                                        { "preserveNullAndEmptyArrays", true }
                                    }),
                                new BsonDocument("$sort", new BsonDocument("CreatedAt", 1)),
                                new BsonDocument("$skip", skip),
                                new BsonDocument("$limit", limit),
                                new BsonDocument("$project",
                                    new BsonDocument
                                    {
                                        { "_id", new BsonDocument("$toString", "$_id") },
                                        { "Content", 1 },
                                        { "AuthorId", 1 },
                                        { "CreatedAt", 1 },
                                        { "ParentCommentId", new BsonDocument("$toString", "$ParentCommentId") },
                                        { "AuthorDisplayName", new BsonDocument("$ifNull", new BsonArray { "$commentAuthor.Profile.DisplayName", "Unknown" }) },
                                        { "AuthorAvatarUrl", new BsonDocument("$ifNull", new BsonArray { "$commentAuthor.Profile.AvatarUrl", "" }) }
                                    })
                            }
                        },
                        { "as", "Comments" }
                    }),
                // Chiếu dữ liệu cuối cùng
                new BsonDocument("$project",
                    new BsonDocument
                    {
                        { "_id", new BsonDocument("$toString", "$_id") },
                        { "Title", 1 },
                        { "Content", 1 },
                        { "AuthorId", 1 },
                        { "CreatedAt", 1 },
                        { "AuthorDisplayName", new BsonDocument("$ifNull", new BsonArray { "$author.Profile.DisplayName", "Unknown" }) },
                        { "AuthorAvatarUrl", new BsonDocument("$ifNull", new BsonArray { "$author.Profile.AvatarUrl", "" }) },
                        { "Comments", 1 }
                    })
            };
            var result = await _context.Topics.Aggregate<TopicDetailViewModel>(pipeline).FirstOrDefaultAsync();
            return new ApiSuccessResult<TopicDetailViewModel>(result);
        }
        //public async Task<ApiResult<List<TopicViewModel>>> GetTopics()
        //{
        //    var pipeline = new List<BsonDocument>
        //    {
        //        // Kết hợp với collection Users dựa trên AuthorId
        //        new BsonDocument("$lookup",
        //            new BsonDocument
        //            {
        //                { "from", "Users" },
        //                { "localField", "AuthorId" },
        //                { "foreignField", "_id" },
        //                { "as", "author" }
        //            }),
        //        // Giải nén mảng author
        //        new BsonDocument("$unwind",
        //            new BsonDocument
        //            {
        //                { "path", "$author" },
        //                { "preserveNullAndEmptyArrays", true } // Giữ topic nếu không tìm thấy author
        //            }),
        //        // Chiếu dữ liệu mong muốn
        //        new BsonDocument("$project",
        //            new BsonDocument
        //            {
        //                { "_id", new BsonDocument("$toString", "$_id") }, // Chuyển _id thành chuỗi
        //                { "Title", 1 },
        //                { "Content", 1 },
        //                { "AuthorId", 1 },
        //                { "CreatedAt", 1 },
        //                { "UpdatedAt", 1 },
        //                { "CommentCount", 1 },
        //                { "AuthorDisplayName", new BsonDocument("$ifNull", new BsonArray { "$author.Profile.DisplayName", "Unknown" }) },
        //                { "AuthorAvatarUrl", new BsonDocument("$ifNull", new BsonArray { "$author.Profile.AvatarUrl", "" }) }
        //            })
        //    };
        //    var result = await _context.Topics.Aggregate<TopicViewModel>(pipeline).ToListAsync();

        //    return new ApiSuccessResult<List<TopicViewModel>>(result);
        //}
    }
}
