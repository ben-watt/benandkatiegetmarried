using benandkatiegetmarried.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using benandkatiegetmarried.Models;
using FluentValidation.Results;
using benandkatiegetmarried.DAL.Rsvp.RsvpCommands;

namespace benandkatiegetmarried.UseCases.Rsvp
{
    public class RsvpHandler : IHandler<RsvpRequest, RsvpResponse>
    {
        private IValidator<RSVP> _rsvpValidator;
        private IRsvpCommands _rsvpCommands;

        public RsvpHandler(IValidator<Models.RSVP> rsvpValidator
            , IRsvpCommands rsvpCommands)
        {
            _rsvpValidator = rsvpValidator;
            _rsvpCommands = rsvpCommands;
        }
        public RsvpResponse Handle(RsvpRequest request)
        {
            IList<ValidationResult> validationResult = new List<ValidationResult>();
            foreach (var rsvp in request.RSVPs)
            {
                validationResult.Add(_rsvpValidator.Validate(rsvp));
            }
            if(validationResult.Any(x => x.IsValid == false))
            {
                return new RsvpResponse() { IsValid = false, Errors = (List<ValidationFailure>)validationResult.SelectMany(X => X.Errors) };
            }
            _rsvpCommands.Create(request.RSVPs);
            return new RsvpResponse() { IsValid = true };
        }
    }
}
