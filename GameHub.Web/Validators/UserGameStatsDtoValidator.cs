using FluentValidation;
using GameHub.Web.Models.DtoModels;

namespace GameHub.Web.Validators
{
    public class UserGameStatsDtoValidator : AbstractValidator<UserGameStatsDto>
    {
        public UserGameStatsDtoValidator()
        {
            RuleFor(ugs => ugs.Time).NotNull();
            RuleFor(ugs => ugs.DifficultyId).NotEmpty();
        }
    }
}
