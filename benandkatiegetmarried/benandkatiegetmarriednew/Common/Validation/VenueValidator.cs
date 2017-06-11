using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using benandkatiegetmarried.Models;
using FluentValidation.Results;

namespace benandkatiegetmarried.Common.Validation
{
    public class VenueValidator : AbstractValidator<Venue>, IValidator<Venue>
    {
        public VenueValidator()
        {
            RuleFor(venue => venue.Id).NotEmpty().WithMessage("A venue must have a venue Id");
            RuleFor(venue => venue.Name).NotEmpty().WithMessage("A venue must have a name");
            RuleFor(venue => venue.Postcode).NotEmpty().WithMessage("A postcode must be supplied");
            RuleFor(venue => venue.AddressLine1).NotEmpty().WithMessage("You must supply an address for the venue");
        }
    }
}
