using System.Collections;
using System.Collections.Generic;

namespace benandkatiegetmarried.UseCases.Rsvp
{
    public class RsvpRequest
    {
        public IEnumerable<Models.RSVP> RSVPs { get; set; }
    }
}