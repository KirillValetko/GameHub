using System.Text.Json;

namespace GameHub.BLL.Models
{
    public class GameDifficultyModel : BaseModel
    {
        public string DifficultyName { get; set; }
        public int DifficultyValue { get; set; }
        public string GameId { get; set; }
        public Object DifficultyParameters { get; set; }
    }
}
