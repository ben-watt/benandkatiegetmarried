using benandkatiegetmarried.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.Validation
{
    class InviteValidator : AbstractValidator<Invite>
    {
        public InviteValidator()
        {
            RuleFor(i => i.Guests).NotEmpty();
            RuleFor(i => i.Id).NotNull().NotEmpty();
            RuleFor(i => i.Greeting).NotNull().NotEmpty();
            RuleFor(i => i.Password).NotNull().NotEmpty().MinimumLength(20);
        }
    }
}
