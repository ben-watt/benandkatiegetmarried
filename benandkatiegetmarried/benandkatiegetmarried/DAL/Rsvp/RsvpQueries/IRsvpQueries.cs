using benandkatiegetmarried.Models;
using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.DAL.Rsvp.RsvpQueries
{
    public interface IRsvpQueries
    {
        IEnumerable<Models.Rsvp> GetAll();
        IEnumerable<Models.Rsvp> GetByGuestIds(IEnumerable<Guid> guestIds);
    }
}