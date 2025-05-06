using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.Paging;
using MrKatsuWebAPI.DTO.PostRequest;
using MrKatsuWebAPI.DTO.ReplyRequest;

namespace MrKatsuWebAPI.Application.Replies 
{
    public interface IReplyService
    {
        Task<ApiResult<int>> CreateReply(ReplyRequest request, int userId);
        Task<ApiResult<bool>> UpdateReply(int id, ReplyRequest request, int userId);
        Task<ApiResult<bool>> DeleteReply(int id, int userId);
        Task<ApiResult<ReplyViewModel>> GetReplyById(int id);
        Task<ApiResult<PagedResult<ReplyViewModel>>> GetReplies(int postId, PagingRequest request);
    }
}
