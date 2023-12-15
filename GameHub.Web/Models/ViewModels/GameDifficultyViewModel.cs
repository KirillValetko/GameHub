using System.Text.Json;

namespace GameHub.Web.Models.ViewModels
{
    public class GameDifficultyViewModel : BaseViewModel
    {
        public string DifficultyName { get; set; }
        public int DifficultyValue { get; set; }
        public string GameId { get; set; }
        public Object DifficultyParameters { get; set; }
    }
}
