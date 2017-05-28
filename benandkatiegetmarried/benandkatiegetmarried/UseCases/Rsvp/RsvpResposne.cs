using FluentValidation.Results;
using System.Collections.Generic;

namespace benandkatiegetmarried.UseCases.Rsvp
{
    public class RsvpResponse
    {
        public bool IsValid { get; set; }
        public IList<ValidationFailure> Errors { get; set; }
    }
}