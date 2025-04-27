using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.Settings;

namespace MrKatsuWebAPI.DataAccess
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);

            // Đăng ký ánh xạ cho IdentityUser để sử dụng Id làm _id
            if (!BsonClassMap.IsClassMapRegistered(typeof(IdentityUser)))
            {
                BsonClassMap.RegisterClassMap<IdentityUser<string>>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(u => u.Id)
                      .SetSerializer(new StringSerializer(BsonType.String));
                });
            }
        }
        public IMongoCollection<ApplicationUser> Users => _database.GetCollection<ApplicationUser>("Users");
        public IMongoCollection<IdentityRole> Roles => _database.GetCollection<IdentityRole>("Roles");
        public IMongoDatabase Database => _database;
    }
}
