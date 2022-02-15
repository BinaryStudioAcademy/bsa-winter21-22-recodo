using FluentValidation;
using Recodo.Common.Dtos.User;

namespace Recodo.Common.Validators
{
    public class LoginUserValidator : AbstractValidator<LoginUserDTO>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).Length(8, 32);
        }
    }
}
