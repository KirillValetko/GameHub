using GameHub.Common.Models;
using MongoDB.Driver.Linq;

namespace GameHub.Common.Helpers.Interfaces
{
    public interface IPaginationHelper<T>
        where T : class
    {
        Task<PaginationResponse<T>> PaginateAsync(IMongoQueryable<T> source, int? pageNumber, int? limit);
    }
}
