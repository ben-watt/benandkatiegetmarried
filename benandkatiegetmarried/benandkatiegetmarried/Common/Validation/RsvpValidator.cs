using benandkatiegetmarried.DAL.Guest.Queries;
using benandkatiegetmarried.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.Validation
{
    public class RsvpValidator : AbstractValidator<RSVP> , IValidator<RSVP>
    {
        public RsvpValidator()
        {
            RuleFor(x => x.GuestId).NotNull().NotEmpty().WithMessage("A guest Id must be on the rsvp");
        }
    }
}
