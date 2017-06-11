using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using benandkatiegetmarried.UseCases.Login;

namespace benandkatiegetmarried.Common.Validation
{
    public class GuestLoginValidator : AbstractValidator<GuestLoginRequest>, IValidator<GuestLoginRequest>
    {
        public GuestLoginValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be null or empty");
            RuleFor(x => x.SecurityCode).NotEmpty().WithMessage("Security Code cannot be null or empty");
        }
    }
}
