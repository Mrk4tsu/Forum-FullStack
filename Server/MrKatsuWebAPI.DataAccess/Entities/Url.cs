namespace MrKatsuWebAPI.DataAccess.Entities
{
    public class Url
    {
        public int Id { get; set; }
        public string UrlString { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int ModId { get; set; }
        public Mod Mod { get; set; } = null!;
    }
}
