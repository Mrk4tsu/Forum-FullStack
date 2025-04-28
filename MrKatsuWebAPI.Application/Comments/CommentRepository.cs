using MongoDB.Driver;
using MrKatsuWebAPI.DataAccess;
using MrKatsuWebAPI.DataAccess.Entities;

namespace MrKatsuWebAPI.Application.Comments
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MongoDbContext _context;

        public CommentRepository(MongoDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Comment comment)
        {
            await _context.Comments.InsertOneAsync(comment);
            await _context.Topics.UpdateOneAsync(
                t => t.Id == comment.TopicId,
                Builders<Topic>.Update.Inc(t => t.CommentCount, 1));
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Comments.UpdateOneAsync(
                c => c.Id == id,
                Builders<Comment>.Update.Set(c => c.IsDeleted, true));
        }

        public async Task DislikeCommentAsync(string commentId, string userId)
        {
            await _context.Comments.UpdateOneAsync(
               c => c.Id == commentId,
               Builders<Comment>.Update
                   .AddToSet(c => c.Dislikes, userId)
                   .Pull(c => c.Likes, userId));
        }

        public async Task<Comment> GetByIdAsync(string id)
        {
            return await _context.Comments.Find(c => c.Id == id && !c.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task<List<Comment>> GetByTopicIdAsync(string topicId)
        {
            return await _context.Comments.Find(c => c.TopicId == topicId && !c.IsDeleted).ToListAsync();
        }

        public async Task LikeCommentAsync(string commentId, string userId)
        {
            await _context.Comments.UpdateOneAsync(
               c => c.Id == commentId,
               Builders<Comment>.Update
                   .AddToSet(c => c.Likes, userId)
                   .Pull(c => c.Dislikes, userId));
        }

        public async Task UpdateAsync(string id, Comment comment)
        {
            await _context.Comments.ReplaceOneAsync(c => c.Id == id, comment);
        }
    }
}
