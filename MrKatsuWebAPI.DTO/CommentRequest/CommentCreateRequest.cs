namespace MrKatsuWebAPI.DTO.CommentRequest
{
    public class CommentCreateRequest
    {
        public string TopicId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ParentCommentId { get; set; } = string.Empty;
    }
}
