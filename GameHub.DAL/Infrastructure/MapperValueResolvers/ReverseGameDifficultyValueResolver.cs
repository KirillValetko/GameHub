using System.Text.Json;
using AutoMapper;
using GameHub.DAL.DataModels;
using GameHub.DAL.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace GameHub.DAL.Infrastructure.MapperValueResolvers
{
    public class ReverseGameDifficultyValueResolver : IValueResolver<GameDifficultyDataModel, GameDifficulty, BsonDocument>
    {
        public BsonDocument Resolve(GameDifficultyDataModel source, GameDifficulty destination, BsonDocument destMember,
            ResolutionContext context)
        {
            var jsonDoc = JsonSerializer.Serialize(source.DifficultyParameters);
            var bsonDoc = BsonSerializer.Deserialize<BsonDocument>(jsonDoc);

            return bsonDoc;
        }
    }
}
