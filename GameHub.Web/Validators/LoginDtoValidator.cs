using FluentValidation;
using GameHub.Web.Models.DtoModels;

namespace GameHub.Web.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(l => l.Login).NotEmpty();
            RuleFor(l => l.Password).NotEmpty();
        }
    }
}
