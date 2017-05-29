using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.DAL.GuestEventDetails.Queries
{
    public interface IGuestEventDetailsQueries<TKey>
    {
        Models.Event GetEventDetails(TKey eventId);
        IEnumerable<Models.Venue> GetVenueDetails(TKey eventId);
        IEnumerable<Models.Guest> GetFeaturedGuests(TKey eventId);
        IEnumerable<Models.Guest> GetGuestsOnInvite(TKey inviteId);
    }
}