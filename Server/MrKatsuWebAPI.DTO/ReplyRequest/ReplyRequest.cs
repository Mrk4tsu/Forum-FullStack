namespace MrKatsuWebAPI.DTO.ReplyRequest
{
    public class ReplyRequest
    {
        public int PostId { get; set; }
        public string Content { get; set; } = string.Empty;
        public int? ParentReplyId { get; set; }
    }
}
