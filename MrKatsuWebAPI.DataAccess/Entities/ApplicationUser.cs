using Microsoft.AspNetCore.Identity;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MrKatsuWebAPI.DataAccess.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [BsonElement("FullName")]
        public string FullName { get; set; }
    }
}
