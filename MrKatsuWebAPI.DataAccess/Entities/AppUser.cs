using Microsoft.AspNetCore.Identity;

namespace MrKatsuWebAPI.DataAccess.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Avatar { get; set; } = string.Empty;
        public DateTime TimeCreated { get; set; } = DateTime.Now;
        public DateTime TimeUpdated { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public List<Post> Posts { get; set; }
        public List<Mod> Mods { get; set; }
        public List<Reaction> Reactions { get; set; }
        public List<Reply> Replies { get; set; }
    }
}
