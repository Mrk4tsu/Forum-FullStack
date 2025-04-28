using MrKatsuWebAPI.DataAccess.Entities;

namespace MrKatsuWebAPI.Application.Comments
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetByTopicIdAsync(string topicId);
        Task<Comment> GetByIdAsync(string id);
        Task CreateAsync(Comment comment);
        Task UpdateAsync(string id, Comment comment);
        Task DeleteAsync(string id);
        Task LikeCommentAsync(string commentId, string userId);
        Task DislikeCommentAsync(string commentId, string userId);
    }
}
