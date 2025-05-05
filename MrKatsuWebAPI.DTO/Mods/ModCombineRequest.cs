namespace MrKatsuWebAPI.DTO.Mods
{
    public class ModUpdateCombineRequest : ModCombineRequest
    {
        public List<UrlUpdateRequest>? UpdatedUrls { get; set; }
        public List<int>? UrlIdsToDelete { get; set; }
    }
    public class ModCombineRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsPrivate { get; set; } = false;
        public byte CategoryId { get; set; }

        public List<string>? NewUrls { get; set; }
    }
    public class ModRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsPrivate { get; set; } = false;
        public byte CategoryId { get; set; }
    }
    public class UrlRequest
    {
        public List<string>? Url { get; set; }
    }
    public class UrlUpdateRequest
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
    }
    public class UrlDeleteRequest
    {
        public List<int> UrlId { get; set; }
    }
}
