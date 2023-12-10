using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Models;

namespace GameHub.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TDbModel, TDataModel, TFilter> 
        where TDbModel : BaseDbModel
        where TDataModel : BaseDataModel
        where TFilter : BaseFilter
    {
        Task CreateAsync(TDataModel item);
        Task UpdateAsync(TDataModel item);
        Task DeleteAsync(string id);
        Task<TDataModel> GetByFilterAsync(TFilter filter);
        Task<List<TDataModel>> GetAllByFilterAsync(TFilter filter);
    }
}
