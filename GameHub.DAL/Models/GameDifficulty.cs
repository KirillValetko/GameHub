using System.Text.Json;
using MongoDB.Bson;

namespace GameHub.DAL.Models
{
    public class GameDifficulty : BaseDbModel
    {
        public string DifficultyName { get; set; }
        public int DifficultyValue { get; set; }
        public string GameId { get; set; }
        public BsonDocument DifficultyParameters { get; set; }
    }
}
