using benandkatiegetmarried.Models;
using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.DAL.Rsvp.RsvpQueries
{
    public interface IRsvpQueries
    {
        IEnumerable<RSVP> GetAll();
        IEnumerable<RSVP> GetByGuestIds(IEnumerable<Guid> guestIds);
    }
}