using AutoMapper;
using GameHub.BLL.Models;
using GameHub.BLL.Services.Interfaces;
using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Models;
using GameHub.DAL.Repositories.Interfaces;

namespace GameHub.BLL.Services
{
    public abstract class BaseService<TDbModel, TDataModel, TModel, TFilter> :
        IBaseService<TDbModel, TDataModel, TModel, TFilter>
        where TDbModel : BaseDbModel
        where TDataModel : BaseDataModel
        where TModel : BaseModel
        where TFilter : BaseFilter
    {
        protected readonly IBaseRepository<TDbModel, TDataModel, TFilter> _repository;
        protected readonly IMapper _mapper;

        protected BaseService(IBaseRepository<TDbModel, TDataModel, TFilter> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task CreateAsync(TModel item)
        {
            var mappedItem = _mapper.Map<TDataModel>(item);
            await _repository.CreateAsync(mappedItem);
        }

        public virtual async Task UpdateAsync(TModel item)
        {
            var mappedItem = _mapper.Map<TDataModel>(item);
            await _repository.UpdateAsync(mappedItem);
        }

        public virtual async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public virtual async Task<TModel> GetByFilterAsync(TFilter filter)
        {
            var dbItem = await _repository.GetByFilterAsync(filter);
            var mappedItem = _mapper.Map<TModel>(dbItem);

            return mappedItem;
        }

        public virtual async Task<List<TModel>> GetAllByFilterAsync(TFilter filter)
        {
            var dbItems = await _repository.GetAllByFilterAsync(filter);
            var mappedItems = _mapper.Map<List<TModel>>(dbItems);

            return mappedItems;
        }
    }
}
