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
    public class RsvpValidator : AbstractValidator<Rsvp> , IValidator<Rsvp>
    {
        public RsvpValidator()
        {
            RuleFor(x => x.Responses.Select(r => r.GuestId)).NotNull().NotEmpty().WithMessage("A guest Ids must be on the rsvp");
        }
    }
}
