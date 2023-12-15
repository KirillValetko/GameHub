using FluentValidation;
using GameHub.Web.Models.DtoModels;

namespace GameHub.Web.Validators
{
    public class GameDifficultyDtoValidator : AbstractValidator<GameDifficultyDto>
    {
        public GameDifficultyDtoValidator()
        {
            RuleFor(gd => gd.DifficultyName).NotEmpty();
            RuleFor(gd => gd.DifficultyValue).NotEmpty();
            RuleFor(gd => gd.GameId).NotEmpty();
            RuleFor(gd => gd.DifficultyParameters).NotEmpty();
        }
    }
}
