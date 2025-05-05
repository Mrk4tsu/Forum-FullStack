using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.Paging;
using MrKatsuWebAPI.DTO.PostRequest;

namespace MrKatsuWebAPI.Application.Posts
{
    public interface IPostService
    {
        Task<ApiResult<PagedResult<PostViewModel>>> GetPosts(PagingRequest request);
        Task<ApiResult<PostDetailViewModel>> GetPostById(int id, PagingRequest request);
        Task<ApiResult<PagedResult<ReplyViewModel>>> GetRepliesByPostId(int postId, PagingRequest request);
        Task<ApiResult<int>> CreatePost(PostRequest request, int userId);
        Task<ApiResult<bool>> UpdatePost(int id, PostRequest request, int userId);
        Task<ApiResult<bool>> DeletePost(int id, int userId);
    }
}
