using AutoMapper;
using GameHub.Common.Constants;
using GameHub.Common.Helpers.Interfaces;
using GameHub.Common.Models;
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
        protected readonly IPaginationHelper<TDbModel> _paginationHelper;
        protected readonly IMapper _mapper;

        protected BaseRepository(IPaginationHelper<TDbModel> paginationHelper,
            IMapper mapper, 
            IOptions<BaseGameHubDbSettings<TDbModel>> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<TDbModel>(settings.Value.CollectionName);
            _paginationHelper = paginationHelper;
            _mapper = mapper;
        }

        public virtual async Task CreateAsync(TDataModel item)
        {
            var mappedItem = _mapper.Map<TDbModel>(item);
            PrepareForCreation(mappedItem);
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

        public async Task<PaginationResponse<TDataModel>> GetPaginatedAsync(PaginationRequest<TFilter> request)
        {
            var source = ConstructFilter(request.Filter);

            var response = await _paginationHelper.PaginateAsync(source, request.PageNumber, request.Limit);
            var mappedResponse = _mapper.Map<PaginationResponse<TDataModel>>(response);

            return mappedResponse;
        }

        protected virtual void PrepareForCreation(TDbModel item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();
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
