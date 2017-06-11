using benandkatiegetmarried.UseCases.Login;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.Validation
{
    public class UserLoginValidator : AbstractValidator<UserLoginRequest> , IValidator<UserLoginRequest>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username cannot be null or empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be null or empty");
        }
    }
}
