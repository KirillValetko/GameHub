namespace GameHub.DAL.Filters
{
    public class UserGameStatsFilter : BaseFilter
    {
        public bool? IsMinTime { get; set; }
        public string GameId { get; set; }
        public string DifficultyId { get; set; }
        public string UserId { get; set; }
    }
}
