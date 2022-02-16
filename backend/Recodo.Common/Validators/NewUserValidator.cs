using FluentValidation;
using Recodo.Common.Dtos.User;

namespace Recodo.Common.Validators
{
    public class NewUserValidator : AbstractValidator<NewUserDTO>
    {
        public NewUserValidator()
        {
            RuleFor(x => x.WorkspaceName).Length(2,20);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).Length(8, 32);
        }
    }
}
