using System.Collections.Generic;

namespace benandkatiegetmarried.DAL.Rsvp.RsvpCommands
{
    public interface IRsvpCommands
    {
        void Create(IEnumerable<Models.RSVP> rsvp);
    }
}