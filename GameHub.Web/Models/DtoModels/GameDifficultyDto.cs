using System.Text.Json;

namespace GameHub.Web.Models.DtoModels
{
    public class GameDifficultyDto : BaseDto
    {
        public string DifficultyName { get; set; }
        public int DifficultyValue { get; set; }
        public string GameId { get; set; }
        public Object DifficultyParameters { get; set; }
    }
}
