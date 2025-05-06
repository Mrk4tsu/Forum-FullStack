using FluentValidation;

namespace MrKatsuWebAPI.DTO.Authorize.Validator
{
    public class RegisterRequestValidator : AbstractValidator<RegisterModel>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress().WithMessage("Email invalid");
            RuleFor(x => x.Password).NotEmpty()
              .MinimumLength(6).WithMessage("Password is at least 6 character ")
              .Matches(@"^\S*$").WithMessage("Password cannot have space");
            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Confirm password not match");
                }
            });
        }
    }
}
