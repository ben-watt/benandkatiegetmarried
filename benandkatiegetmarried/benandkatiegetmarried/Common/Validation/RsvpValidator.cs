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
            RuleFor(x => x).Must(r => r.Id != Guid.Empty).WithMessage("An rsvp cannot have an empty guid as it's Id");
            RuleFor(x => x).Must(rsvp => rsvp.Responses.All(x => x.RsvpId == rsvp.Id)).WithMessage("All Responses must have an association to the rsvp through it's Id");
            RuleFor(x => x.Responses).Must(res => res.Count() > 0).WithMessage("At least one response should be supplied as part of an Rsvp");
            RuleFor(x => x.Responses.Select(r => r.Response)).NotEmpty().WithMessage("A response must be supplied with an RSVP");
            RuleFor(x => x.Responses.Select(r => r.RsvpId)).NotEmpty().WithMessage("A a response must contain an rsvpId");
            RuleFor(x => x.Responses.Select(r => r.GuestId)).NotEmpty().WithMessage("A guest Ids must be on the rsvp");
            RuleFor(x => x.InviteId).NotEmpty().WithMessage("A invite Id must be on the rsvp");
            RuleFor(x => x.EventId).NotEmpty().WithMessage("EventId cannot be null or empty");
        }
    }
}
