using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.DAL.GuestEventDetails.Queries
{
    public interface IGuestEventDetailsQueries
    {
        Models.Event GetEventDetails(Guid eventId);
        IEnumerable<Models.Venue> GetVenueDetails(Guid eventId);
        IEnumerable<Models.Guest> GetFeaturedGuests(Guid eventId);
        IEnumerable<Models.Guest> GetGuestsOnInvite(Guid inviteId);
    }
}