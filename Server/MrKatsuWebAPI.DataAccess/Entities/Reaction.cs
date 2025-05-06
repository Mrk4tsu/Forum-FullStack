namespace MrKatsuWebAPI.DataAccess.Entities
{
    public class Reaction
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int ModId { get; set; }
        public Mod Mod { get; set; } = null!;
        public int UserId { get; set; }
        public AppUser User { get; set; } = null!;
    }
}
