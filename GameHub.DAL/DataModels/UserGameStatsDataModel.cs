namespace GameHub.DAL.DataModels
{
    public class UserGameStatsDataModel : BaseDataModel
    {
        public int NumberOfGames { get; set; }
        public int NumberOfWins { get; set; }
        public TimeSpan BestTime { get; set; }
        public string GameId { get; set; }
        public string DifficultyId { get; set; }
        public string UserId { get; set; }
    }
}
