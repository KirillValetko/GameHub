using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameHub.DAL.Models
{
    public class UserGameStats : BaseDbModel
    {
        public int NumberOfGames { get; set; }

        public int NumberOfWins { get; set; }

        public TimeSpan BestTime { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string DifficultyId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonIgnoreIfNull]
        public User User { get; set; }
    }
}
