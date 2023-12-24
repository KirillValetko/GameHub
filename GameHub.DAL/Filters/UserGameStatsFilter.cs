namespace GameHub.DAL.Filters
{
    public class UserGameStatsFilter : BaseFilter
    {
        public string DifficultyId { get; set; }
        public string UserId { get; set; }
        public bool? IncludeUser { get; set; }
    }
}
