using System.Text.Json;

namespace GameHub.DAL.DataModels
{
    public class GameDifficultyDataModel : BaseDataModel
    {
        public string DifficultyName { get; set; }
        public int DifficultyValue { get; set; }
        public string GameId { get; set; }
        public Object DifficultyParameters { get; set; }
    }
}
