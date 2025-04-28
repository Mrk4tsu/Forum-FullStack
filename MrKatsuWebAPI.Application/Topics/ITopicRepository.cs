using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.TopicRequest;

namespace MrKatsuWebAPI.Application.Topics
{
    public interface ITopicRepository
    {
        Task<List<Topic>> GetAllAsync();
        Task<Topic> GetByIdAsync(string id);
        Task CreateAsync(TopicCreateRequest request, string userId);
        Task UpdateAsync(string id, Topic topic);
        Task DeleteAsync(string id);
    }
}
