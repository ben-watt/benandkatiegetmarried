using benandkatiegetmarried.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using benandkatiegetmarried.Models;
using FluentValidation.Results;
using benandkatiegetmarried.DAL.Rsvp.RsvpCommands;
using FluentValidation;
using benandkatiegetmarried.DAL.Rsvp.RsvpQueries;

namespace benandkatiegetmarried.UseCases.Rsvp
{
    public class RsvpHandler : IHandler<RsvpRequest, RsvpResponse>
    {
        private IValidator<RSVP> _rsvpValidator;
        private IRsvpCommands _rsvpCommands;
        private IRsvpQueries _rsvpQueries;

        public RsvpHandler(IValidator<RSVP> rsvpValidator
            , IRsvpCommands rsvpCommands
            , IRsvpQueries rsvpQueries)
        {
            _rsvpValidator = rsvpValidator;
            _rsvpCommands = rsvpCommands;
            _rsvpQueries = rsvpQueries;
        }
        public RsvpResponse Handle(RsvpRequest request)
        {
            IList<ValidationResult> validationResult = ValidateRSVP(request);
            var guestIds = request.RSVPs.Select(x => x.GuestId);
            if (ErrorsDuringValidation(validationResult)
                || RsvpExistsForGuests(guestIds))
            {
                return new RsvpResponse() { IsValid = false, Errors = (List<ValidationFailure>)validationResult.SelectMany(x => x.Errors) };
            }
            _rsvpCommands.Create(request.RSVPs);
            return new RsvpResponse() { IsValid = true };
        }

        private bool RsvpExistsForGuests(IEnumerable<Guid> guestIds)
        {
            var result = _rsvpQueries.GetByGuestIds(guestIds);
            if(result.Count() > 0)
            {
                return true; 
            }
            return false;
        }

        private static bool ErrorsDuringValidation(IList<ValidationResult> validationResult)
        {
            return validationResult.Any(x => x.IsValid == false);
        }

        private IList<ValidationResult> ValidateRSVP(RsvpRequest request)
        {
            IList<ValidationResult> validationResult = new List<ValidationResult>();
            foreach (var rsvp in request.RSVPs)
            {
                validationResult.Add(_rsvpValidator.Validate(rsvp));
            }
            return validationResult;
        }
    }
}
