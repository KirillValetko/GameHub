using AutoMapper;
using GameHub.Common.Constants;
using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Infrastructure.DbSettings;
using GameHub.DAL.Models;
using GameHub.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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

        protected BaseRepository(IMapper mapper, 
            IOptions<BaseGameHubDbSettings<TDbModel>> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<TDbModel>(settings.Value.CollectionName);
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

            _mapper.Map(item, dbItem);
            await _collection.ReplaceOneAsync(i => i.Id == item.Id, dbItem);
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
            var source = ConstructFilter(filter);

            var item = await source.FirstOrDefaultAsync();
            var mappedItem = _mapper.Map<TDataModel>(item);

            return mappedItem;
        }

        public async Task<List<TDataModel>> GetAllByFilterAsync(TFilter filter)
        {
            var source = ConstructFilter(filter);

            var items = await source.ToListAsync();
            var mappedItems = _mapper.Map<List<TDataModel>>(items);
            
            return mappedItems;
        }

        private IMongoQueryable<TDbModel> ConstructFilter(TFilter filter)
        {
            var source = _collection.AsQueryable();
            filter ??= new TFilter();

            if (!string.IsNullOrEmpty(filter.Id))
            {
                source = source.Where(i => i.Id.Equals(filter.Id));
            }

            source = AddFilterConditions(source, filter);

            return source;
        }

        protected abstract IMongoQueryable<TDbModel> AddFilterConditions(IMongoQueryable<TDbModel> source, TFilter filter);
    }
}
