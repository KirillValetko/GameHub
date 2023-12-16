namespace GameHub.Common.Models
{
    public class PaginationRequest<T>
        where T : class
    {
        public int? PageNumber { get; set; }
        public int? Limit { get; set; }
        public T Filter { get; set; }
    }
}
