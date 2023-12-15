namespace GameHub.BLL.Models
{
    public class UserGameStatsModel : BaseModel
    {
        public int NumberOfGames { get; set; }
        public int NumberOfWins { get; set; }
        public TimeSpan BestTime { get; set; }
        public string GameId { get; set; }
        public string DifficultyId { get; set; }
        public string UserId { get; set; }
    }
}
