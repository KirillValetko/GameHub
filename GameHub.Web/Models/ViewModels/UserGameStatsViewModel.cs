namespace GameHub.Web.Models.ViewModels
{
    public class UserGameStatsViewModel : BaseViewModel
    {
        public int NumberOfGames { get; set; }
        public int NumberOfWins { get; set; }
        public TimeSpan BestTime { get; set; }
        public string DifficultyId { get; set; }
        public string UserId { get; set; }

        public UserViewModel User { get; set; }
    }
}
