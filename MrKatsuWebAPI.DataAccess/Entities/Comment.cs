using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MrKatsuWebAPI.DataAccess.Entities
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string TopicId { get; set; } = string.Empty;
        public string AuthorId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ParentCommentId { get; set; }
        public List<string> Likes { get; set; } = new List<string>();
        public List<string> Dislikes { get; set; } = new List<string>();
        public bool IsDeleted { get; set; } = false;
    }
}
