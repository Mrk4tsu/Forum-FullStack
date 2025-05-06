using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.Mods;
using MrKatsuWebAPI.DTO.Paging;

namespace MrKatsuWebAPI.Application.Mods
{
    public interface IModService
    {
        Task<ApiResult<PagedResult<ModViewModel>>> GetMods(ModPagingRequest request);
        Task<ApiResult<ModDetailViewModel>> GetModById(int id, PagingRequest request);
        Task<ApiResult<PagedResult<ReactViewModel>>> GetReactByModId(int modId, PagingRequest request);
        Task<ApiResult<ModInternalViewModel>> GetModInternalById(int id);
        Task<ApiResult<int>> CreateMod(ModCombineRequest request, int userId);
        Task<ApiResult<int>> UpdateMod(int id, ModUpdateCombineRequest request, int userId);
        Task<ApiResult<bool>> DeleteMod(int id, int userId);
        Task<ApiResult<int>> DeleteUrls(int modId, UrlDeleteRequest request, int userId);
        Task<ApiResult<int>> CreateReaction(ReactionRequest request, int userId);
        Task<ApiResult<bool>> UpdateReaction(int id, ReactionRequest request, int userId);
        Task<ApiResult<bool>> DeleteReaction(int id, int userId);
    }
}
