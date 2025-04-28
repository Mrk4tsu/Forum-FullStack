using MongoDB.Driver;
using MrKatsuWebAPI.DataAccess;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.TopicRequest;

namespace MrKatsuWebAPI.Application.Topics
{
    public class TopicRepository : ITopicRepository
    {
        private readonly MongoDbContext _context;
        public TopicRepository(MongoDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(TopicCreateRequest request, string userId)
        {
            var newTopic = new Topic
            {
                Title = request.Title,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AuthorId = userId,
                CommentCount = 0,
            };
            await _context.Topics.InsertOneAsync(newTopic);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Topics.DeleteOneAsync(t => t.Id == id);
        }

        public async Task<List<Topic>> GetAllAsync()
        {
            return await _context.Topics.Find(_ => true).ToListAsync();
        }

        public async Task<Topic> GetByIdAsync(string id)
        {
            return await _context.Topics.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, Topic topic)
        {
            await _context.Topics.ReplaceOneAsync(t => t.Id == id, topic);
        }
    }
}
