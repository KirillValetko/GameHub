using AutoMapper;
using GameHub.DAL.DataModels;
using GameHub.DAL.Models;
using MongoDB.Bson.Serialization;

namespace GameHub.DAL.Infrastructure.MapperValueResolvers
{
    public class GameDifficultyValueResolver : IValueResolver<GameDifficulty, GameDifficultyDataModel, Object>
    {
        public object Resolve(GameDifficulty source, GameDifficultyDataModel destination, object destMember,
            ResolutionContext context)
        {
            var obj = BsonSerializer.Deserialize<Object>(source.DifficultyParameters);

            return obj;
        }
    }
}
