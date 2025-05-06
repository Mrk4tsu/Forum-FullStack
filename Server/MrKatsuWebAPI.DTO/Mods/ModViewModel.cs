namespace MrKatsuWebAPI.DTO.Mods
{
    public class ModInternalViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsPrivate { get; set; } = false;
        public byte CategoryId { get; set; }
        public int AuthorId { get; set; }
        public List<UrlViewModel> Urls { get; set; } = new List<UrlViewModel>();
    }
    public class ModViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public string AuthorDisplayName { get; set; } = string.Empty;
        public string AuthorAvatarUrl { get; set; } = string.Empty;
        public string Category { get; set;} = string.Empty;
        public bool IsPrivate { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class ModDetailViewModel : ModViewModel
    {
        public string Content { get; set; } = string.Empty;
        public List<UrlViewModel> Urls { get; set; } = new List<UrlViewModel>();
    }
    public class UrlViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
    }
    public class ReactViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public string AuthorDisplayName { get; set; } = string.Empty;
        public string AuthorAvatarUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
