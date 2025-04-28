using Microsoft.AspNetCore.Identity;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using AspNetCore.Identity.MongoDbCore.Models;

namespace MrKatsuWebAPI.DataAccess.Entities
{
    public class ApplicationUser :  MongoIdentityUser<string>
    {
        public Profile Profile { get; set; } = new Profile();
    }
    public class Profile
    {
        public string DisplayName { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
