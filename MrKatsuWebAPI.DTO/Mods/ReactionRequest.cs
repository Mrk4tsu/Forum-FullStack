namespace MrKatsuWebAPI.DTO.Mods
{
    public class ReactionRequest
    {
        public int ModId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
