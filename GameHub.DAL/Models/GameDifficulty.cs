using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameHub.DAL.Models
{
    public class GameDifficulty : BaseDbModel
    {
        public string DifficultyName { get; set; }
        public int DifficultyValue { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string GameId { get; set; }
        public BsonDocument DifficultyParameters { get; set; }
    }
}
