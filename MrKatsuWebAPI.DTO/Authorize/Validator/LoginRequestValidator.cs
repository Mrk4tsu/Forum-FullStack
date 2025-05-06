using FluentValidation;

namespace MrKatsuWebAPI.DTO.Authorize.Validator
{
    public class LoginRequestValidator : AbstractValidator<LoginModel>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
