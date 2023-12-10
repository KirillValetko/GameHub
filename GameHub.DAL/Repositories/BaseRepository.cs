using AutoMapper;
using GameHub.Common.Constants;
using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Models;
using GameHub.DAL.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameHub.DAL.Repositories
{
    public abstract class BaseRepository<TDbModel, TDataModel, TFilter> :
        IBaseRepository<TDbModel, TDataModel, TFilter>
        where TDbModel : BaseDbModel
        where TDataModel : BaseDataModel
        where TFilter : BaseFilter, new()
    {
        protected readonly IMongoCollection<TDbModel> _collection;
        protected readonly IMapper _mapper;

        protected BaseRepository(IMapper mapper)
        {
            var mongoClient = new MongoClient();
            var mongoDatabase = mongoClient.GetDatabase("");
            _collection = mongoDatabase.GetCollection<TDbModel>("");
            _mapper = mapper;
        }

        public virtual async Task CreateAsync(TDataModel item)
        {
            var mappedItem = _mapper.Map<TDbModel>(item);
            await _collection.InsertOneAsync(mappedItem);
        }

        public virtual async Task UpdateAsync(TDataModel item)
        {
            var dbItem = await _collection.Find(i => i.Id == item.Id).FirstOrDefaultAsync();

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            var mappedItem = _mapper.Map<TDbModel>(item);
            await _collection.ReplaceOneAsync(i => i.Id == item.Id, mappedItem);
        }

        public virtual async Task DeleteAsync(string id)
        {
            var dbItem = await _collection.Find(i => i.Id == id).FirstOrDefaultAsync();

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            await _collection.DeleteOneAsync(i => i.Id == id);
        }

        public async Task<TDataModel> GetByFilterAsync(TFilter filter)
        {
            var bsonFilter = ConstructFilter(filter);

            var item = await _collection.Find(bsonFilter).FirstOrDefaultAsync();
            var mappedItem = _mapper.Map<TDataModel>(item);

            return mappedItem;
        }

        public async Task<List<TDataModel>> GetAllByFilterAsync(TFilter filter)
        {
            var bsonFilter = ConstructFilter(filter);

            var items = await _collection.FindAsync(bsonFilter);
            var mappedItems = _mapper.Map<List<TDataModel>>(items);
            
            return mappedItems;
        }

        private BsonDocument ConstructFilter(TFilter filter)
        {
            var bsonFilter = new BsonDocument();
            filter ??= new TFilter();

            if (!string.IsNullOrEmpty(filter.Id))
            {
                bsonFilter.Add("_id", filter.Id);
            }

            bsonFilter = AddFilterConditions(bsonFilter, filter);

            return bsonFilter;
        }

        protected abstract BsonDocument AddFilterConditions(BsonDocument bsonFilter, TFilter filter);
    }
}
