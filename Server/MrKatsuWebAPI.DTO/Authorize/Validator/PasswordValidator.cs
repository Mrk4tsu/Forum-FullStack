using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrKatsuWebAPI.DTO.Authorize.Validator
{
    public class PasswordValidator : AbstractValidator<ChangePasswordRequest>
    {
        public PasswordValidator()
        {
            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.NewPassword == request.CurrentPassword)
                {
                    context.AddFailure("Mật khẩu mới không thể giống mật khẩu cũ");
                }
            });
            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.NewPassword != request.ConfirmNewPassword)
                {
                    context.AddFailure("Mật khẩu không khớp");
                }
            });
        }
    }
}
