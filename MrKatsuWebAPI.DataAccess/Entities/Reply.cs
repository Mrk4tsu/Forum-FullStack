namespace MrKatsuWebAPI.DataAccess.Entities
{
    public class Reply
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
        public int UserId { get; set; }
        public AppUser User { get; set; } = null!;
        public int? ParentId { get; set; }
        public Reply? Parent { get; set; }
        public List<Reply> Children { get; set; } = new List<Reply>();
    }
}
