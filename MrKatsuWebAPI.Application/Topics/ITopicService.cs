using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.CommentRequest;
using MrKatsuWebAPI.DTO.TopicRequest;

namespace MrKatsuWebAPI.Application.Topics
{
    public interface ITopicService
    {
        Task<ApiResult<List<TopicViewModel>>> GetTopics(int skip = 0, int limit = 10);
        Task<ApiResult<TopicDetailViewModel>> GetTopicDetail(string topicId, int skip = 0, int limit = 10);
        Task<ApiResult<string>> Comment(CommentCreateRequest request, string userId);
    }
}
