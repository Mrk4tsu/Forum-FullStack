namespace MrKatsuWebAPI.DTO.PostRequest
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public string AuthorDisplayName { get; set; } = string.Empty;
        public string AuthorAvatarUrl { get; set; } = string.Empty;
        public bool IsLocked { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CommentCount { get; set; }
    }
    public class PostDetailViewModel : PostViewModel
    {
        public string Content { get; set; } = string.Empty;
    }
    public class ReplyViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public string AuthorDisplayName { get; set; } = string.Empty;
        public string AuthorAvatarUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int? ParentReplyId { get; set; }
    }
}
