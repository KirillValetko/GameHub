using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GameHub.DAL.Models
{
    public class BaseDbModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
