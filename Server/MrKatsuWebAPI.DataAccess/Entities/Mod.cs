namespace MrKatsuWebAPI.DataAccess.Entities
{
    public class Mod
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; } = null!;
        public byte CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsPrivate { get; set; }
        public List<Reaction> Reactions { get; set; }
        public List<Url> Urls { get; set; }
    }
}
