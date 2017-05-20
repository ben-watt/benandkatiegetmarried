using benandkatiegetmarried.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.Validation
{
    public class WeddingValidator : AbstractValidator<Wedding>, IValidator<Wedding>
    {
        public WeddingValidator()
        {
            RuleFor(w => w.Bride).NotEmpty().NotNull().WithMessage("A wedding must have a bride");
            RuleFor(w => w.Groom).NotEmpty().NotNull().WithMessage("A wedding must have a groom");
        }
    }
}
