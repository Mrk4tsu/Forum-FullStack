using MrKatsuWebAPI.DTO.Paging;

namespace MrKatsuWebAPI.DTO.Mods
{
    public class ModPagingRequest : PagingRequest
    {
        public string? Username { get; set; } = string.Empty;
        public string? Category { get; set; } = string.Empty;
        public string? KeyWord { get; set; } = string.Empty;
    }
}
