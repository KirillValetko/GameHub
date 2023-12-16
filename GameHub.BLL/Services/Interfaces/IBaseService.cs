using GameHub.BLL.Models;
using GameHub.Common.Models;
using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Models;

namespace GameHub.BLL.Services.Interfaces
{
    public interface IBaseService<TDbModel, TDataModel, TModel, TFilter>
        where TDbModel : BaseDbModel
        where TDataModel : BaseDataModel
        where TModel : BaseModel
        where TFilter : BaseFilter
    {
        Task CreateAsync(TModel item);
        Task UpdateAsync(TModel item);
        Task DeleteAsync(string id);
        Task<TModel> GetByFilterAsync(TFilter filter);
        Task<List<TModel>> GetAllByFilterAsync(TFilter filter);
        Task<PaginationResponse<TModel>> GetPaginatedAsync(PaginationRequest<TFilter> paginationRequest);
    }
}
