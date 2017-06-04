using benandkatiegetmarried.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.Validation
{
    class IValidator : AbstractValidator<Invite> , IValidator<Invite>
    {
        public IValidator()
        {
            RuleFor(i => i.Id).NotEmpty();
            RuleFor(i => i.Greeting).NotEmpty();
            RuleFor(i => i.Password).NotEmpty().MinimumLength(20);
        }
    }
}
