using Microsoft.AspNetCore.Identity;

namespace MrKatsuWebAPI.DataAccess.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public string Description { get; set; } = string.Empty;
    }
}
