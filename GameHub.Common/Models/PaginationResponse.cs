namespace GameHub.Common.Models
{
    public class PaginationResponse<T>
        where T : class
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public List<T> PaginatedData { get; set; }
    }
}
