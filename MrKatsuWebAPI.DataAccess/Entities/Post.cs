namespace MrKatsuWebAPI.DataAccess.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; } = null!;
        public List<Reply> Replies { get; set; }
    }
}
